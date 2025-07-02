using FluentValidation.TestHelper;
using LangVoyageServer.Database;
using LangVoyageServer.Models;
using LangVoyageServer.Requests;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace TestServer.UnitTests;

/// <summary>
/// Unit tests for SqliteStorageService methods focusing on business logic and edge cases
/// </summary>
public class SqliteStorageServiceTests : IDisposable
{
    private readonly LangServerDbContext _context;
    private readonly SqliteStorageService _service;

    public SqliteStorageServiceTests()
    {
        var options = new DbContextOptionsBuilder<LangServerDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new LangServerDbContext(options);
        _service = new SqliteStorageService(_context);

        // Seed with basic test data
        SeedTestData();
    }

    private void SeedTestData()
    {
        var testUser = new UserProfile
        {
            Id = 1,
            Username = "testuser",
            LanguageLevel = "A1"
        };

        var testNouns = new[]
        {
            new LanguageNoun { Id = 1, Noun = "Haus", Article = "das", Level = "A1" },
            new LanguageNoun { Id = 2, Noun = "Auto", Article = "das", Level = "A1" },
            new LanguageNoun { Id = 3, Noun = "Buch", Article = "das", Level = "A2" }
        };

        _context.UserProfiles.Add(testUser);
        _context.Nouns.AddRange(testNouns);
        _context.SaveChanges();
    }

    [Fact]
    public async Task UpsertUserProfileAsync_ShouldReturnNull_WhenBothFieldsAreNull()
    {
        // Arrange
        var request = new UpdateUserRequest
        {
            Username = null,
            LanguageLevel = null
        };

        // Act
        var result = await _service.UpsertUserProfileAsync(1, request);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task UpsertUserProfileAsync_ShouldUpdateExistingUser_WhenUserExists()
    {
        // Arrange
        var request = new UpdateUserRequest
        {
            Username = "updateduser",
            LanguageLevel = "B1"
        };

        // Act
        var result = await _service.UpsertUserProfileAsync(1, request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("updateduser", result.Username);
        Assert.Equal("B1", result.LanguageLevel);
    }

    [Fact]
    public async Task UpsertUserProfileAsync_ShouldCreateNewUser_WhenUserDoesNotExist()
    {
        // Arrange
        var request = new UpdateUserRequest
        {
            Username = "newuser",
            LanguageLevel = "A2"
        };

        // Act
        var result = await _service.UpsertUserProfileAsync(999, request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("newuser", result.Username);
        Assert.Equal("A2", result.LanguageLevel);
    }

    [Fact]
    public async Task UpsertUserProfileAsync_ShouldUpdateOnlyUsername_WhenLanguageLevelIsNull()
    {
        // Arrange
        var request = new UpdateUserRequest
        {
            Username = "partialupdateuser",
            LanguageLevel = null
        };

        // Act
        var result = await _service.UpsertUserProfileAsync(1, request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("partialupdateuser", result.Username);
        Assert.Equal("A1", result.LanguageLevel); // Should keep original level
    }

    [Fact]
    public async Task UpsertUserProfileAsync_ShouldUpdateOnlyLanguageLevel_WhenUsernameIsNull()
    {
        // Arrange
        var request = new UpdateUserRequest
        {
            Username = null,
            LanguageLevel = "C1"
        };

        // Act
        var result = await _service.UpsertUserProfileAsync(1, request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("testuser", result.Username); // Should keep original username
        Assert.Equal("C1", result.LanguageLevel);
    }

    [Fact]
    public async Task GetUserAsync_ShouldReturnUser_WhenUserExists()
    {
        // Act
        var result = await _service.GetUserAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("testuser", result.Username);
        Assert.Equal("A1", result.LanguageLevel);
    }

    [Fact]
    public async Task GetUserAsync_ShouldReturnNull_WhenUserDoesNotExist()
    {
        // Act
        var result = await _service.GetUserAsync(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetNewPractiseNounsAsync_ShouldThrowException_WhenUserDoesNotExist()
    {
        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => 
            _service.GetNewPractiseNounsAsync(999, 10));
        
        Assert.Equal("No user profile found.", exception.Message);
    }

    [Fact]
    public async Task GetNewPractiseNounsAsync_ShouldReturnNounsForUserLevel_WhenUserExists()
    {
        // Act
        var result = await _service.GetNewPractiseNounsAsync(1, 10);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Count > 0);
        Assert.All(result, noun => Assert.Equal("A1", noun.Level));
    }

    [Fact]
    public async Task GetNewPractiseNounsAsync_ShouldRespectLimit_WhenCalledWithLimit()
    {
        // Act
        var result = await _service.GetNewPractiseNounsAsync(1, 1);

        // Assert
        Assert.Single(result);
    }

    [Fact]
    public async Task GetPractiseNounAsync_ShouldReturnNull_WhenProgressDoesNotExist()
    {
        // Act
        var result = await _service.GetPractiseNounAsync(1, 1);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task UpsertNounProgressAsync_ShouldThrowException_WhenUserDoesNotExist()
    {
        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => 
            _service.UpsertNounProgressAsync(999, 1, true));
        
        Assert.Equal("No user profile found.", exception.Message);
    }

    [Fact]
    public async Task UpsertNounProgressAsync_ShouldCreateNewProgress_WhenProgressDoesNotExist()
    {
        // Act
        var result = await _service.UpsertNounProgressAsync(1, 1, true);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.UserProfileId);
        Assert.Equal(1, result.NounId);
        Assert.Equal(1, result.TimeFrame); // Should start at 1 for correct answer
    }

    [Fact]
    public async Task UpsertNounProgressAsync_ShouldIncreaseTimeFrame_WhenAnswerIsCorrect()
    {
        // Arrange - Create initial progress
        await _service.UpsertNounProgressAsync(1, 1, true);

        // Act - Answer correctly again
        var result = await _service.UpsertNounProgressAsync(1, 1, true);

        // Assert
        Assert.Equal(2, result.TimeFrame);
    }

    [Fact]
    public async Task UpsertNounProgressAsync_ShouldDecreaseTimeFrame_WhenAnswerIsIncorrect()
    {
        // Arrange - Create initial progress with higher time frame
        await _service.UpsertNounProgressAsync(1, 1, true);
        await _service.UpsertNounProgressAsync(1, 1, true);

        // Act - Answer incorrectly
        var result = await _service.UpsertNounProgressAsync(1, 1, false);

        // Assert
        Assert.Equal(1, result.TimeFrame); // Should decrease from 2 to 1
    }

    [Fact]
    public async Task UpsertNounProgressAsync_ShouldNotGoBelow0_WhenTimeFrameIsAlready0()
    {
        // Arrange - Create progress at 0
        var progress = new NounProgress
        {
            UserProfileId = 1,
            NounId = 1,
            TimeFrame = 0,
            LastPractised = DateTime.UtcNow
        };
        _context.NounProgresses.Add(progress);
        await _context.SaveChangesAsync();

        // Act - Answer incorrectly
        var result = await _service.UpsertNounProgressAsync(1, 1, false);

        // Assert
        Assert.Equal(0, result.TimeFrame); // Should stay at 0
    }

    [Fact]
    public async Task DeleteNounProgressAsync_ShouldReturnZero_WhenProgressDoesNotExist()
    {
        // Act
        var result = await _service.DeleteNounProgressAsync(1, 999);

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public async Task DeleteNounProgressAsync_ShouldReturnOne_WhenProgressExists()
    {
        // Arrange - Create progress
        await _service.UpsertNounProgressAsync(1, 1, true);

        // Act
        var result = await _service.DeleteNounProgressAsync(1, 1);

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public async Task UpdateAllNounProgressAsync_ShouldCreateProgressForAllNouns()
    {
        // Act
        var result = await _service.UpdateAllNounProgressAsync(1);

        // Assert
        Assert.NotEmpty(result);
        Assert.All(result, progress => 
        {
            Assert.Equal(1, progress.UserProfileId);
            Assert.Equal(1, progress.TimeFrame);
        });
    }

    [Fact]
    public async Task DeleteAllNounProgressAsync_ShouldRemoveAllUserProgress()
    {
        // Arrange - Create some progress
        await _service.UpsertNounProgressAsync(1, 1, true);
        await _service.UpsertNounProgressAsync(1, 2, true);

        // Act
        await _service.DeleteAllNounProgressAsync(1);

        // Assert
        var remainingProgress = _context.NounProgresses.Where(p => p.UserProfileId == 1);
        Assert.Empty(remainingProgress);
    }

    [Fact]
    public async Task GetLearningProgress_ShouldReturnProgressWithCorrectUserInfo()
    {
        // Act
        var result = await _service.GetLearningProgress(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("testuser", result.Username);
        Assert.Equal("A1", result.LanguageLevel);
        Assert.True(result.TotalNouns > 0);
        Assert.NotNull(result.NounProgresses);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}