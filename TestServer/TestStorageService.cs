using LangVoyageServer.Database;
using LangVoyageServer.Models;
using LangVoyageServer.Requests;
using Microsoft.Extensions.DependencyInjection;

namespace TestServer;

[Collection("Sequential")]
public class TestStorageService : IClassFixture<TestWebApplicationFactory<Program>>
{
    private readonly TestWebApplicationFactory<Program> _factory;
    private static bool _databaseInitialized;
    private static readonly object _lock = new object();

    public TestStorageService(TestWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        lock (_lock)
        {
            if (!_databaseInitialized)
            {
                using var scope = _factory.Services.CreateScope();
                var (context, service) = Utilities.SeedDatabase(scope, deleteDatabase: true).GetAwaiter().GetResult();

                service.UpsertUserProfileAsync(1, new UpdateUserRequest()
                    {
                        Username = "spaceman",
                        LanguageLevel = "C2"
                    }
                ).GetAwaiter().GetResult();

                context.SaveChanges();

                _databaseInitialized = true;
            }
        }
    }

    /// <summary>
    /// Verifies that GetUserAsync returns a valid user when provided with an existing user ID.
    /// This test ensures the basic user retrieval functionality works correctly for valid inputs.
    /// </summary>
    [Fact]
    public async Task GetUserAsync_WithValidId_ReturnsUser()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();

        // Act
        var result = await service.GetUserAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.NotNull(result.Username);
        Assert.NotNull(result.LanguageLevel);
    }

    /// <summary>
    /// Verifies that GetUserAsync returns null when provided with a non-existent user ID.
    /// This test ensures proper handling of invalid user ID scenarios and prevents system errors.
    /// </summary>
    [Fact]
    public async Task GetUserAsync_WithInvalidId_ReturnsNull()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();

        // Act
        var result = await service.GetUserAsync(99999);

        // Assert
        Assert.Null(result);
    }

    /// <summary>
    /// Verifies that UpsertUserProfileAsync returns null when both username and language level are null.
    /// This test ensures the service handles incomplete update requests gracefully without making changes.
    /// </summary>
    [Fact]
    public async Task UpsertUserProfileAsync_WithNullRequest_ReturnsNull()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();
        var request = new UpdateUserRequest(); // Both fields are null

        // Act
        var result = await service.UpsertUserProfileAsync(1, request);

        // Assert
        Assert.Null(result);
    }

    /// <summary>
    /// Verifies that UpsertUserProfileAsync successfully updates user profile data when provided with valid information.
    /// This test ensures the core user profile update functionality works correctly with complete data.
    /// </summary>
    [Fact]
    public async Task UpsertUserProfileAsync_WithValidData_UpdatesUser()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();
        var request = new UpdateUserRequest
        {
            Username = "test_user",
            LanguageLevel = "B1"
        };

        // Act
        var result = await service.UpsertUserProfileAsync(1, request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("test_user", result.Username);
        Assert.Equal("B1", result.LanguageLevel);
    }

    /// <summary>
    /// Verifies that GetNewPractiseNounsAsync throws an exception when provided with an invalid user ID.
    /// This test ensures proper error handling for non-existent users attempting to access learning content.
    /// </summary>
    [Fact]
    public async Task GetNewPractiseNounsAsync_WithInvalidUserId_ThrowsException()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(async () =>
            await service.GetNewPractiseNounsAsync(99999));
    }

    /// <summary>
    /// Verifies that GetNewPractiseNounsAsync returns valid noun data when provided with a valid user ID.
    /// This test ensures that users can successfully retrieve practice nouns with proper data structure.
    /// </summary>
    [Fact]
    public async Task GetNewPractiseNounsAsync_WithValidUserId_ReturnsNouns()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();

        // Act
        var result = await service.GetNewPractiseNounsAsync(1, 5);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Count <= 5);
        foreach (var noun in result)
        {
            Assert.NotNull(noun.Noun);
            Assert.NotNull(noun.Article);
            Assert.NotNull(noun.Level);
        }
    }

    /// <summary>
    /// Verifies that UpsertNounProgressAsync throws an exception when provided with an invalid user ID.
    /// This test ensures proper error handling when attempting to update progress for non-existent users.
    /// </summary>
    [Fact]
    public async Task UpsertNounProgressAsync_WithInvalidUserId_ThrowsException()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(async () =>
            await service.UpsertNounProgressAsync(99999, 1, true));
    }

    /// <summary>
    /// Verifies that UpsertNounProgressAsync creates new progress record with TimeFrame=1 when no previous progress exists.
    /// This test ensures proper initialization of the spaced repetition learning algorithm for new nouns.
    /// </summary>
    [Fact]
    public async Task UpsertNounProgressAsync_CreatesNewProgress_WhenNoneExists()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();
        
        // Get a noun to test with
        var nouns = await service.GetNewPractiseNounsAsync(1, 1);
        var noun = nouns.First();

        // Ensure no progress exists for this noun
        await service.DeleteNounProgressAsync(1, noun.Id);

        // Act
        var result = await service.UpsertNounProgressAsync(1, noun.Id, true);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.UserProfileId);
        Assert.Equal(noun.Id, result.NounId);
        Assert.Equal(1, result.TimeFrame);
    }

    /// <summary>
    /// Verifies that DeleteNounProgressAsync successfully removes progress records and returns the count of deleted items.
    /// This test ensures proper cleanup functionality for resetting individual noun progress.
    /// </summary>
    [Fact]
    public async Task DeleteNounProgressAsync_RemovesProgress()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();
        
        // Get a noun and create progress for it
        var nouns = await service.GetNewPractiseNounsAsync(1, 1);
        var noun = nouns.First();
        await service.UpsertNounProgressAsync(1, noun.Id, true);

        // Act
        var deleteCount = await service.DeleteNounProgressAsync(1, noun.Id);

        // Assert
        Assert.Equal(1, deleteCount);
        
        // Verify it's deleted
        var progress = await service.GetPractiseNounAsync(1, noun.Id);
        Assert.Null(progress);
    }

    /// <summary>
    /// Verifies that DeleteNounProgressAsync returns zero when attempting to delete non-existent progress.
    /// This test ensures graceful handling of delete operations on already removed or never-created progress records.
    /// </summary>
    [Fact]
    public async Task DeleteNounProgressAsync_WithNonExistentProgress_ReturnsZero()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();

        // Act
        var deleteCount = await service.DeleteNounProgressAsync(1, 99999);

        // Assert
        Assert.Equal(0, deleteCount);
    }

    /// <summary>
    /// Verifies that GetLearningProgress returns comprehensive learning statistics including user info and progress distribution.
    /// This test ensures the learning dashboard functionality works correctly and provides meaningful analytics to users.
    /// </summary>
    [Fact]
    public async Task GetLearningProgress_ReturnsValidProgress()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();

        // Act
        var result = await service.GetLearningProgress(1);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Username);
        Assert.NotNull(result.LanguageLevel);
        Assert.True(result.TotalNouns > 0);
        Assert.NotNull(result.NounProgresses);
        Assert.True(result.NounProgresses.Length > 0);
    }
}