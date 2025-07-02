using LangVoyageServer.Database;
using LangVoyageServer.Models;
using LangVoyageServer.Requests;

namespace TestServer;

/// <summary>
/// Unit tests for the IStorageService implementation
/// These tests focus on testing the service layer in isolation
/// </summary>
[Collection("Sequential")]
public class StorageServiceTests : IClassFixture<TestWebApplicationFactory<Program>>
{
    private readonly TestWebApplicationFactory<Program> _factory;

    public StorageServiceTests(TestWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        DatabaseTestHelper.InitializeDatabase(factory);
    }

    [Fact]
    public async Task GetUserAsync_WithValidId_ReturnsUser()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();

        // Act
        var user = await service.GetUserAsync(TestConstants.DefaultUserId);

        // Assert
        TestHelpers.AssertUserProfile(user, TestConstants.DefaultUserId);
    }

    [Fact]
    public async Task GetUserAsync_WithInvalidId_ReturnsNull()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();

        // Act
        var user = await service.GetUserAsync(TestConstants.NonExistentUserId);

        // Assert
        Assert.Null(user);
    }

    [Theory]
    [InlineData("NewUsername", "A1")]
    [InlineData("AnotherUser", "C2")]
    [InlineData(null, "B1")] // Only update language level
    [InlineData("OnlyUsername", null)] // Only update username
    public async Task UpsertUserProfileAsync_WithValidData_UpdatesCorrectly(string? username, string? languageLevel)
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();
        var updateRequest = new UpdateUserRequest
        {
            Username = username,
            LanguageLevel = languageLevel
        };

        // Act
        var updatedUser = await service.UpsertUserProfileAsync(TestConstants.DefaultUserId, updateRequest);

        // Assert
        Assert.NotNull(updatedUser);
        Assert.Equal(TestConstants.DefaultUserId, updatedUser.Id);
        
        if (username != null)
            Assert.Equal(username, updatedUser.Username);
            
        if (languageLevel != null)
            Assert.Equal(languageLevel, updatedUser.LanguageLevel);
    }

    [Fact]
    public async Task UpsertUserProfileAsync_WithNonExistentUser_ReturnsNull()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();
        var updateRequest = TestHelpers.CreateValidUpdateUserRequest();

        // Act
        var result = await service.UpsertUserProfileAsync(TestConstants.NonExistentUserId, updateRequest);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetNewPractiseNounsAsync_ReturnsCorrectCount()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();
        await DatabaseTestHelper.CleanupUserProgress(scope, TestConstants.DefaultUserId);

        // Act
        var nouns = await service.GetNewPractiseNounsAsync(TestConstants.DefaultUserId, TestConstants.SmallNounLimit);

        // Assert
        Assert.NotNull(nouns);
        Assert.True(nouns.Count <= TestConstants.SmallNounLimit);
        Assert.True(nouns.Count > 0);
    }

    [Fact]
    public async Task GetNewPractiseNounsAsync_WithInvalidUser_ThrowsException()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => 
            service.GetNewPractiseNounsAsync(TestConstants.NonExistentUserId));
    }

    [Fact]
    public async Task UpsertNounProgressAsync_CreatesNewProgress()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();
        await DatabaseTestHelper.CleanupUserProgress(scope, TestConstants.DefaultUserId);

        var nouns = await service.GetNewPractiseNounsAsync(TestConstants.DefaultUserId, 1);
        var noun = nouns.First();

        // Act
        var progress = await service.UpsertNounProgressAsync(TestConstants.DefaultUserId, noun.Id, true);

        // Assert
        Assert.NotNull(progress);
        Assert.Equal(TestConstants.DefaultUserId, progress.UserProfileId);
        Assert.Equal(noun.Id, progress.NounId);
        Assert.Equal(1, progress.TimeFrame);
        Assert.True(progress.LastPractised > DateTime.MinValue);
    }

    [Fact]
    public async Task UpsertNounProgressAsync_UpdatesExistingProgress()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();
        await DatabaseTestHelper.CleanupUserProgress(scope, TestConstants.DefaultUserId);

        var nouns = await service.GetNewPractiseNounsAsync(TestConstants.DefaultUserId, 1);
        var noun = nouns.First();

        // Create initial progress
        await service.UpsertNounProgressAsync(TestConstants.DefaultUserId, noun.Id, true);

        // Act - Update progress
        var updatedProgress = await service.UpsertNounProgressAsync(TestConstants.DefaultUserId, noun.Id, true);

        // Assert
        Assert.NotNull(updatedProgress);
        Assert.Equal(2, updatedProgress.TimeFrame);
    }

    [Fact]
    public async Task UpsertNounProgressAsync_WithIncorrectAnswer_DecreasesTimeFrame()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();
        await DatabaseTestHelper.CleanupUserProgress(scope, TestConstants.DefaultUserId);

        var nouns = await service.GetNewPractiseNounsAsync(TestConstants.DefaultUserId, 1);
        var noun = nouns.First();

        // Build up time frame
        await service.UpsertNounProgressAsync(TestConstants.DefaultUserId, noun.Id, true); // 1
        await service.UpsertNounProgressAsync(TestConstants.DefaultUserId, noun.Id, true); // 2
        await service.UpsertNounProgressAsync(TestConstants.DefaultUserId, noun.Id, true); // 3

        // Act - Answer incorrectly
        var progress = await service.UpsertNounProgressAsync(TestConstants.DefaultUserId, noun.Id, false);

        // Assert
        Assert.NotNull(progress);
        Assert.Equal(2, progress.TimeFrame); // Should decrease by 1
    }

    [Fact]
    public async Task GetPractiseNounAsync_ReturnsCorrectNoun()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();
        await DatabaseTestHelper.CleanupUserProgress(scope, TestConstants.DefaultUserId);

        var nouns = await service.GetNewPractiseNounsAsync(TestConstants.DefaultUserId, 1);
        var noun = nouns.First();
        await service.UpsertNounProgressAsync(TestConstants.DefaultUserId, noun.Id, true);

        // Act
        var progress = await service.GetPractiseNounAsync(TestConstants.DefaultUserId, noun.Id);

        // Assert
        Assert.NotNull(progress);
        Assert.Equal(TestConstants.DefaultUserId, progress.UserProfileId);
        Assert.Equal(noun.Id, progress.NounId);
    }

    [Fact]
    public async Task GetPractiseNounAsync_WithNonExistentProgress_ReturnsNull()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();

        // Act
        var progress = await service.GetPractiseNounAsync(TestConstants.DefaultUserId, 999999);

        // Assert
        Assert.Null(progress);
    }

    [Fact]
    public async Task DeleteNounProgressAsync_RemovesProgress()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();
        await DatabaseTestHelper.CleanupUserProgress(scope, TestConstants.DefaultUserId);

        var nouns = await service.GetNewPractiseNounsAsync(TestConstants.DefaultUserId, 1);
        var noun = nouns.First();
        await service.UpsertNounProgressAsync(TestConstants.DefaultUserId, noun.Id, true);

        // Act
        var deleted = await service.DeleteNounProgressAsync(TestConstants.DefaultUserId, noun.Id);

        // Assert
        Assert.True(deleted > 0);
        var progress = await service.GetPractiseNounAsync(TestConstants.DefaultUserId, noun.Id);
        Assert.Null(progress);
    }

    [Fact]
    public async Task DeleteAllNounProgressAsync_RemovesAllProgress()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();
        await DatabaseTestHelper.SetupUserWithProgress(scope, TestConstants.DefaultUserId, TestConstants.SmallNounLimit);

        // Act
        await service.DeleteAllNounProgressAsync(TestConstants.DefaultUserId);

        // Assert
        var progress = await service.GetLearningProgress(TestConstants.DefaultUserId);
        Assert.NotNull(progress);
        Assert.All(progress.NounProgresses, count => Assert.Equal(0, count));
    }

    [Fact]
    public async Task UpdateAllNounProgressAsync_CreatesProgressForAllNouns()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();
        await DatabaseTestHelper.CleanupUserProgress(scope, TestConstants.DefaultUserId);

        // Act
        var results = await service.UpdateAllNounProgressAsync(TestConstants.DefaultUserId);

        // Assert
        Assert.NotNull(results);
        Assert.True(results.Count > 0);
        Assert.All(results, progress => 
        {
            Assert.Equal(TestConstants.DefaultUserId, progress.UserProfileId);
            Assert.True(progress.TimeFrame > 0);
        });
    }

    [Fact]
    public async Task GetLearningProgress_ReturnsValidProgress()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();
        await DatabaseTestHelper.SetupUserWithProgress(scope, TestConstants.DefaultUserId, TestConstants.SmallNounLimit);

        // Act
        var progress = await service.GetLearningProgress(TestConstants.DefaultUserId);

        // Assert
        TestHelpers.AssertLearningProgress(progress, TestConstants.DefaultUsername, TestConstants.DefaultLanguageLevel);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task GetNewPractiseNounsAsync_WithInvalidLimit_HandlesGracefully(int limit)
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();

        // Act & Assert
        if (limit <= 0)
        {
            // Should either throw exception or return empty collection
            var result = await service.GetNewPractiseNounsAsync(TestConstants.DefaultUserId, limit);
            // The exact behavior depends on implementation - this test documents it
            Assert.NotNull(result);
        }
    }
}