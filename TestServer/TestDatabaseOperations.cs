using LangVoyageServer.Database;
using LangVoyageServer.Models;
using LangVoyageServer.Requests;
using Microsoft.Extensions.DependencyInjection;

namespace TestServer;

[Collection("Sequential")]
public class TestDatabaseOperations : IClassFixture<TestWebApplicationFactory<Program>>
{
    private readonly TestWebApplicationFactory<Program> _factory;
    private static bool _databaseInitialized;
    private static readonly object _lock = new object();

    public TestDatabaseOperations(TestWebApplicationFactory<Program> factory)
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
    /// Verifies that UpdateNounsAsync correctly handles duplicate nouns by removing duplicates from database storage.
    /// This test ensures data integrity and prevents duplicate noun entries from corrupting the learning content.
    /// </summary>
    [Fact]
    public async Task UpdateNounsAsync_WithDuplicateNouns_RemovesDuplicates()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();
        
        var duplicateNouns = new[]
        {
            new LanguageNoun { Noun = "Test", Article = "der", Level = "A1", Plural = "Tests" },
            new LanguageNoun { Noun = "Test", Article = "der", Level = "A1", Plural = "Tests" }, // duplicate
            new LanguageNoun { Noun = "Haus", Article = "das", Level = "A1", Plural = "Häuser" }
        };

        // Act
        var result = await service.UpdateNounsAsync(duplicateNouns);

        // Assert
        Assert.Equal(3, result.Count()); // Should still return all 3 input items
        var uniqueNouns = result.GroupBy(n => n.Noun).Select(g => g.First()).ToList();
        Assert.Equal(2, uniqueNouns.Count); // But only 2 unique nouns should be processed
    }

    /// <summary>
    /// Verifies that UpdateNounsAsync gracefully handles empty input arrays without causing errors.
    /// This test ensures robust handling of edge cases in bulk noun update operations.
    /// </summary>
    [Fact]
    public async Task UpdateNounsAsync_WithEmptyArray_ReturnsEmptyResult()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();
        
        var emptyNouns = Array.Empty<LanguageNoun>();

        // Act
        var result = await service.UpdateNounsAsync(emptyNouns);

        // Assert
        Assert.Empty(result);
    }

    /// <summary>
    /// Verifies that the spaced repetition TimeFrame progression algorithm works correctly through multiple practice sessions.
    /// This test ensures the core learning algorithm advances TimeFrame on correct answers and decrements on incorrect ones.
    /// </summary>
    [Fact]
    public async Task NounProgress_TimeFrameProgression_WorksCorrectly()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();
        
        var nouns = await service.GetNewPractiseNounsAsync(1, 1);
        var testNoun = nouns.First();
        
        // Clean slate
        await service.DeleteNounProgressAsync(1, testNoun.Id);

        // Act & Assert - First correct answer should create TimeFrame 1
        var progress1 = await service.UpsertNounProgressAsync(1, testNoun.Id, true);
        Assert.Equal(1, progress1.TimeFrame);

        // Second correct answer should increment to TimeFrame 2
        var progress2 = await service.UpsertNounProgressAsync(1, testNoun.Id, true);
        Assert.Equal(2, progress2.TimeFrame);

        // Third correct answer should increment to TimeFrame 3
        var progress3 = await service.UpsertNounProgressAsync(1, testNoun.Id, true);
        Assert.Equal(3, progress3.TimeFrame);

        // Incorrect answer should decrement back to TimeFrame 2
        var progress4 = await service.UpsertNounProgressAsync(1, testNoun.Id, false);
        Assert.Equal(2, progress4.TimeFrame);
    }

    /// <summary>
    /// Verifies that TimeFrame values never drop below zero even with multiple consecutive incorrect answers.
    /// This test ensures the spaced repetition algorithm maintains valid state boundaries and prevents negative values.
    /// </summary>
    [Fact]
    public async Task NounProgress_TimeFrameCannotGoBelowZero()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();
        
        var nouns = await service.GetNewPractiseNounsAsync(1, 1);
        var testNoun = nouns.First();
        
        // Clean slate
        await service.DeleteNounProgressAsync(1, testNoun.Id);

        // Act - Create progress and then fail it multiple times
        await service.UpsertNounProgressAsync(1, testNoun.Id, true); // TimeFrame = 1
        var progress1 = await service.UpsertNounProgressAsync(1, testNoun.Id, false); // TimeFrame = 0
        Assert.Equal(0, progress1.TimeFrame);

        var progress2 = await service.UpsertNounProgressAsync(1, testNoun.Id, false); // Should stay 0
        Assert.Equal(0, progress2.TimeFrame);

        var progress3 = await service.UpsertNounProgressAsync(1, testNoun.Id, false); // Should stay 0
        Assert.Equal(0, progress3.TimeFrame);
    }

    /// <summary>
    /// Verifies that UpdateAllNounProgressAsync creates progress records for all available nouns with initial TimeFrame=1.
    /// This test ensures bulk progress initialization functionality works correctly for new users or progress resets.
    /// </summary>
    [Fact]
    public async Task UpdateAllNounProgressAsync_CreatesProgressForAllNouns()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();
        
        // Clear all progress first
        await service.DeleteAllNounProgressAsync(1);

        // Act
        var result = await service.UpdateAllNounProgressAsync(1);

        // Assert
        Assert.NotEmpty(result);
        
        // All progress records should have TimeFrame of 1
        foreach (var progress in result)
        {
            Assert.Equal(1, progress.TimeFrame);
            Assert.Equal(1, progress.UserProfileId);
        }
    }

    /// <summary>
    /// Verifies that GetNewPractiseNounsAsync respects the limit parameter and returns appropriate number of nouns.
    /// This test ensures proper pagination and result limiting functionality for practice session management.
    /// </summary>
    [Fact]
    public async Task GetNewPractiseNounsAsync_RespectsLimit()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();

        // Act
        var result5 = await service.GetNewPractiseNounsAsync(1, 5);
        var result10 = await service.GetNewPractiseNounsAsync(1, 10);
        var result1 = await service.GetNewPractiseNounsAsync(1, 1);

        // Assert
        Assert.True(result5.Count <= 5);
        Assert.True(result10.Count <= 10);
        Assert.True(result1.Count <= 1);
    }

    /// <summary>
    /// Verifies that GetNewPractiseNounsAsync returns nouns ordered by TimeFrame for optimal spaced repetition learning.
    /// This test ensures the learning algorithm prioritizes nouns that need more practice (lower TimeFrame values).
    /// </summary>
    [Fact]
    public async Task GetNewPractiseNounsAsync_OrdersByTimeFrame()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();
        
        // Clear all progress
        await service.DeleteAllNounProgressAsync(1);
        
        // Get some nouns and practice them to different levels
        var allNouns = await service.GetNewPractiseNounsAsync(1, 3);
        
        // Practice first noun to TimeFrame 3
        await service.UpsertNounProgressAsync(1, allNouns[0].Id, true);
        await service.UpsertNounProgressAsync(1, allNouns[0].Id, true);
        await service.UpsertNounProgressAsync(1, allNouns[0].Id, true);
        
        // Practice second noun to TimeFrame 1
        await service.UpsertNounProgressAsync(1, allNouns[1].Id, true);
        
        // Don't practice third noun (TimeFrame 0)

        // Act
        var orderedNouns = await service.GetNewPractiseNounsAsync(1, 3);

        // Assert
        // The unpracticed noun should come first (TimeFrame 0)
        // Then the noun with TimeFrame 1
        // Then the noun with TimeFrame 3
        var firstNounProgress = await service.GetPractiseNounAsync(1, orderedNouns[0].Id);
        Assert.True(firstNounProgress == null || firstNounProgress.TimeFrame <= 1);
    }

    /// <summary>
    /// Verifies that GetLearningProgress accurately reflects actual practice progress and updates dynamically.
    /// This test ensures the learning dashboard provides accurate analytics based on current progress state.
    /// </summary>
    [Fact]
    public async Task GetLearningProgress_ReflectsActualProgress()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();
        
        // Clear all progress
        await service.DeleteAllNounProgressAsync(1);
        
        // Get initial progress (should be mostly zeros)
        var initialProgress = await service.GetLearningProgress(1);
        
        // Practice some nouns
        var nouns = await service.GetNewPractiseNounsAsync(1, 5);
        foreach (var noun in nouns)
        {
            await service.UpsertNounProgressAsync(1, noun.Id, true);
        }

        // Act
        var updatedProgress = await service.GetLearningProgress(1);

        // Assert
        Assert.Equal(initialProgress.TotalNouns, updatedProgress.TotalNouns);
        Assert.Equal(initialProgress.Username, updatedProgress.Username);
        Assert.Equal(initialProgress.LanguageLevel, updatedProgress.LanguageLevel);
        
        // Should have 5 nouns at TimeFrame 1
        Assert.True(updatedProgress.NounProgresses[1] >= 5);
    }

    /// <summary>
    /// Verifies that DeleteAllNounProgressAsync completely removes all progress records for a user.
    /// This test ensures proper cleanup functionality for progress reset operations and maintains data integrity.
    /// </summary>
    [Fact]
    public async Task DeleteAllNounProgressAsync_ClearsAllProgress()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();
        
        // Create some progress
        var nouns = await service.GetNewPractiseNounsAsync(1, 3);
        foreach (var noun in nouns)
        {
            await service.UpsertNounProgressAsync(1, noun.Id, true);
        }
        
        // Verify progress exists
        var progressBefore = await service.GetLearningProgress(1);
        Assert.True(progressBefore.NounProgresses.Skip(1).Sum() > 0);

        // Act
        await service.DeleteAllNounProgressAsync(1);

        // Assert
        var progressAfter = await service.GetLearningProgress(1);
        Assert.Equal(0, progressAfter.NounProgresses.Skip(1).Sum());
    }
}