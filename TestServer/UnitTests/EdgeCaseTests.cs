using LangVoyageServer.Database;
using LangVoyageServer.Models;
using LangVoyageServer.Requests;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace TestServer.UnitTests;

/// <summary>
/// Edge case and error scenario tests for improved coverage
/// </summary>
public class EdgeCaseTests : IDisposable
{
    private readonly LangServerDbContext _context;
    private readonly SqliteStorageService _service;

    public EdgeCaseTests()
    {
        var options = new DbContextOptionsBuilder<LangServerDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new LangServerDbContext(options);
        _service = new SqliteStorageService(_context);
    }

    [Fact]
    public async Task UpdateNounsAsync_ShouldHandleEmptyArray()
    {
        // Arrange
        var emptyData = Array.Empty<LanguageNoun>();

        // Act
        var result = await _service.UpdateNounsAsync(emptyData);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task UpdateNounsAsync_ShouldHandleDuplicateNouns()
    {
        // Arrange
        var duplicateData = new[]
        {
            new LanguageNoun { Noun = "Haus", Article = "das", Level = "A1", Plural = "Häuser" },
            new LanguageNoun { Noun = "Haus", Article = "das", Level = "A1", Plural = "Houses" }, // Different plural
        };

        // Act
        var result = await _service.UpdateNounsAsync(duplicateData);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count()); // Should return original array
        
        // Check that only one noun was actually saved
        var savedNouns = await _context.Nouns.Where(n => n.Noun == "Haus").ToListAsync();
        Assert.Single(savedNouns);
        Assert.Equal("Häuser", savedNouns.First().Plural); // Should keep the first one
    }

    [Fact]
    public async Task UpdateNounsAsync_ShouldUpdateExistingNoun()
    {
        // Arrange - First add a noun
        var initialNoun = new LanguageNoun { Noun = "Auto", Article = "das", Level = "A1", Plural = "Autos" };
        _context.Nouns.Add(initialNoun);
        await _context.SaveChangesAsync();

        // Update the noun
        var updatedData = new[]
        {
            new LanguageNoun { Noun = "Auto", Article = "das", Level = "A1", Plural = "Cars" }
        };

        // Act
        await _service.UpdateNounsAsync(updatedData);

        // Assert
        var updatedNoun = await _context.Nouns.FirstAsync(n => n.Noun == "Auto");
        Assert.Equal("Cars", updatedNoun.Plural);
    }

    [Fact]
    public async Task GetNewPractiseNounsAsync_ShouldReturnEmptyList_WhenNoNounsAtUserLevel()
    {
        // Arrange
        var user = new UserProfile { Id = 1, Username = "test", LanguageLevel = "C2" };
        _context.UserProfiles.Add(user);
        
        // Add nouns at different level
        var noun = new LanguageNoun { Id = 1, Noun = "Test", Article = "das", Level = "A1" };
        _context.Nouns.Add(noun);
        await _context.SaveChangesAsync();

        // Act
        var result = await _service.GetNewPractiseNounsAsync(1, 10);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetNewPractiseNounsAsync_ShouldHandleZeroLimit()
    {
        // Arrange
        var user = new UserProfile { Id = 1, Username = "test", LanguageLevel = "A1" };
        _context.UserProfiles.Add(user);
        
        var noun = new LanguageNoun { Id = 1, Noun = "Test", Article = "das", Level = "A1" };
        _context.Nouns.Add(noun);
        await _context.SaveChangesAsync();

        // Act
        var result = await _service.GetNewPractiseNounsAsync(1, 0);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task UpsertNounProgressAsync_ShouldHandleNewProgressWithIncorrectAnswer()
    {
        // Arrange
        var user = new UserProfile { Id = 1, Username = "test", LanguageLevel = "A1" };
        _context.UserProfiles.Add(user);
        await _context.SaveChangesAsync();

        // Act - First attempt is incorrect
        var result = await _service.UpsertNounProgressAsync(1, 1, false);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(0, result.TimeFrame); // Should start at 0 for incorrect answer
    }

    [Fact]
    public async Task UpsertNounProgressAsync_ShouldUpdateLastPractisedTime()
    {
        // Arrange
        var user = new UserProfile { Id = 1, Username = "test", LanguageLevel = "A1" };
        _context.UserProfiles.Add(user);
        await _context.SaveChangesAsync();

        var beforeTime = DateTime.UtcNow;

        // Act
        var result = await _service.UpsertNounProgressAsync(1, 1, true);

        // Assert
        Assert.True(result.LastPractised >= beforeTime);
        Assert.True(result.LastPractised <= DateTime.UtcNow);
    }

    [Fact]
    public async Task GetLearningProgress_ShouldHandleUserWithNoProgress()
    {
        // Arrange
        var user = new UserProfile { Id = 1, Username = "test", LanguageLevel = "A1" };
        _context.UserProfiles.Add(user);
        
        // Add some nouns but no progress
        var nouns = new[]
        {
            new LanguageNoun { Id = 1, Noun = "Test1", Article = "das", Level = "A1" },
            new LanguageNoun { Id = 2, Noun = "Test2", Article = "der", Level = "A1" }
        };
        _context.Nouns.AddRange(nouns);
        await _context.SaveChangesAsync();

        // Act
        var result = await _service.GetLearningProgress(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("test", result.Username);
        Assert.Equal("A1", result.LanguageLevel);
        Assert.Equal(2, result.TotalNouns);
        Assert.NotNull(result.NounProgresses);
        Assert.Empty(result.NounProgresses); // No progress entries
    }

    [Fact]
    public async Task UpdateAllNounProgressAsync_ShouldHandleUserWithNoNouns()
    {
        // Arrange
        var user = new UserProfile { Id = 1, Username = "test", LanguageLevel = "C2" };
        _context.UserProfiles.Add(user);
        
        // Add nouns at different level only
        var noun = new LanguageNoun { Id = 1, Noun = "Test", Article = "das", Level = "A1" };
        _context.Nouns.Add(noun);
        await _context.SaveChangesAsync();

        // Act
        var result = await _service.UpdateAllNounProgressAsync(1);

        // Assert
        Assert.Empty(result); // No nouns at user's level
    }

    [Fact]
    public async Task DeleteNounProgressAsync_ShouldHandleNonExistentUser()
    {
        // Act
        var result = await _service.DeleteNounProgressAsync(999, 1);

        // Assert
        Assert.Equal(0, result); // No progress to delete
    }

    [Fact]
    public async Task DeleteAllNounProgressAsync_ShouldHandleUserWithNoProgress()
    {
        // Arrange
        var user = new UserProfile { Id = 1, Username = "test", LanguageLevel = "A1" };
        _context.UserProfiles.Add(user);
        await _context.SaveChangesAsync();

        // Act - Should not throw
        await _service.DeleteAllNounProgressAsync(1);

        // Assert - Should complete without error
        var progress = _context.NounProgresses.Where(p => p.UserProfileId == 1);
        Assert.Empty(progress);
    }

    [Fact]
    public async Task UpsertUserProfileAsync_ShouldCreateUserWithNullLanguageLevel()
    {
        // Arrange
        var request = new UpdateUserRequest
        {
            Username = "testuser",
            LanguageLevel = null
        };

        // Act
        var result = await _service.UpsertUserProfileAsync(999, request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("testuser", result.Username);
        Assert.Null(result.LanguageLevel);
    }

    [Fact]
    public async Task UpsertUserProfileAsync_ShouldCreateUserWithNullUsername()
    {
        // Arrange
        var request = new UpdateUserRequest
        {
            Username = null,
            LanguageLevel = "A1"
        };

        // Act
        var result = await _service.UpsertUserProfileAsync(999, request);

        // Assert
        Assert.NotNull(result);
        Assert.Null(result.Username);
        Assert.Equal("A1", result.LanguageLevel);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}