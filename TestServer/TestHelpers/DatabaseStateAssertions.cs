using LangVoyageServer.Database;
using LangVoyageServer.Models;
using LangVoyageServer.Requests;
using Microsoft.Extensions.DependencyInjection;

namespace TestServer.TestHelpers;

/// <summary>
/// Helper class for asserting database state in tests.
/// Provides convenient methods to verify data integrity and state changes.
/// </summary>
public class DatabaseStateAssertions
{
    private readonly IServiceScope _scope;

    public DatabaseStateAssertions(IServiceScope scope)
    {
        _scope = scope;
    }

    /// <summary>
    /// Gets the storage service for direct database operations.
    /// </summary>
    public IStorageService StorageService => _scope.ServiceProvider.GetRequiredService<IStorageService>();

    /// <summary>
    /// Asserts that a user exists with the specified properties.
    /// </summary>
    public async Task AssertUserExistsAsync(int userId, string? expectedUsername = null, string? expectedLanguageLevel = null)
    {
        var user = await StorageService.GetUserAsync(userId);
        Assert.NotNull(user);
        
        if (expectedUsername != null)
        {
            Assert.Equal(expectedUsername, user.Username);
        }
        
        if (expectedLanguageLevel != null)
        {
            Assert.Equal(expectedLanguageLevel, user.LanguageLevel);
        }
    }

    /// <summary>
    /// Asserts that a user does not exist.
    /// </summary>
    public async Task AssertUserDoesNotExistAsync(int userId)
    {
        var user = await StorageService.GetUserAsync(userId);
        Assert.Null(user);
    }

    /// <summary>
    /// Asserts that noun progress exists with the specified properties.
    /// </summary>
    public async Task AssertNounProgressExistsAsync(int userId, int nounId, int? expectedTimeFrame = null, DateTime? expectedLastPractised = null)
    {
        var progress = await StorageService.GetPractiseNounAsync(userId, nounId);
        Assert.NotNull(progress);
        
        if (expectedTimeFrame.HasValue)
        {
            Assert.Equal(expectedTimeFrame.Value, progress.TimeFrame);
        }
        
        if (expectedLastPractised.HasValue)
        {
            // Allow for small time differences (within 1 second)
            var timeDifference = Math.Abs((progress.LastPractised - expectedLastPractised.Value).TotalSeconds);
            Assert.True(timeDifference <= 1, $"Expected LastPractised to be within 1 second of {expectedLastPractised}, but was {progress.LastPractised}");
        }
    }

    /// <summary>
    /// Asserts that noun progress does not exist for the specified user and noun.
    /// </summary>
    public async Task AssertNounProgressDoesNotExistAsync(int userId, int nounId)
    {
        var progress = await StorageService.GetPractiseNounAsync(userId, nounId);
        Assert.Null(progress);
    }

    /// <summary>
    /// Asserts that the user has no progress records (all deleted).
    /// </summary>
    public async Task AssertUserHasNoProgressAsync(int userId)
    {
        var nouns = await StorageService.GetNewPractiseNounsAsync(userId, int.MaxValue);
        var nounsWithProgress = new List<LanguageNoun>();
        
        foreach (var noun in nouns)
        {
            var progress = await StorageService.GetPractiseNounAsync(userId, noun.Id);
            if (progress != null)
            {
                nounsWithProgress.Add(noun);
            }
        }
        
        Assert.Empty(nounsWithProgress);
    }

    /// <summary>
    /// Gets the learning progress and asserts it has expected values.
    /// </summary>
    public async Task<LearningProgressResponse> AssertLearningProgressAsync(int userId, string? expectedUsername = null, string? expectedLanguageLevel = null, int? expectedTotalNouns = null)
    {
        var progress = await StorageService.GetLearningProgress(userId);
        Assert.NotNull(progress);
        
        if (expectedUsername != null)
        {
            Assert.Equal(expectedUsername, progress.Username);
        }
        
        if (expectedLanguageLevel != null)
        {
            Assert.Equal(expectedLanguageLevel, progress.LanguageLevel);
        }
        
        if (expectedTotalNouns.HasValue)
        {
            Assert.Equal(expectedTotalNouns.Value, progress.TotalNouns);
        }
        
        return progress;
    }

    /// <summary>
    /// Asserts that the learning progress has the expected count at a specific time frame.
    /// </summary>
    public async Task AssertProgressCountAtTimeFrameAsync(int userId, int timeFrame, int expectedCount)
    {
        var progress = await StorageService.GetLearningProgress(userId);
        Assert.NotNull(progress);
        Assert.True(timeFrame < progress.NounProgresses.Length, $"TimeFrame {timeFrame} is out of bounds for progress array");
        Assert.Equal(expectedCount, progress.NounProgresses[timeFrame]);
    }

    /// <summary>
    /// Asserts that the specified nouns are returned for practice in the expected order.
    /// </summary>
    public async Task AssertPracticeNounsOrderAsync(int userId, int limit, params int[] expectedNounIds)
    {
        var nouns = await StorageService.GetNewPractiseNounsAsync(userId, limit);
        var actualIds = nouns.Select(n => n.Id).ToArray();
        
        Assert.Equal(expectedNounIds.Length, Math.Min(actualIds.Length, expectedNounIds.Length));
        
        for (int i = 0; i < Math.Min(actualIds.Length, expectedNounIds.Length); i++)
        {
            Assert.Equal(expectedNounIds[i], actualIds[i]);
        }
    }

    /// <summary>
    /// Asserts that all returned nouns match the user's language level.
    /// </summary>
    public async Task AssertNounsMatchUserLevelAsync(int userId, int limit = 20)
    {
        var user = await StorageService.GetUserAsync(userId);
        Assert.NotNull(user);
        
        var nouns = await StorageService.GetNewPractiseNounsAsync(userId, limit);
        Assert.All(nouns, noun => Assert.Equal(user.LanguageLevel, noun.Level));
    }

    /// <summary>
    /// Creates test data for a noun progress scenario.
    /// </summary>
    public async Task CreateNounProgressAsync(int userId, int nounId, int timeFrame, DateTime? lastPractised = null)
    {
        // First get the noun to practice (this will create initial progress if it doesn't exist)
        await StorageService.UpsertNounProgressAsync(userId, nounId, true);
        
        // Then adjust to the desired time frame
        var currentProgress = await StorageService.GetPractiseNounAsync(userId, nounId);
        Assert.NotNull(currentProgress);
        
        // Adjust time frame by practicing correctly or incorrectly
        while (currentProgress.TimeFrame < timeFrame)
        {
            await StorageService.UpsertNounProgressAsync(userId, nounId, true);
            currentProgress = await StorageService.GetPractiseNounAsync(userId, nounId);
            Assert.NotNull(currentProgress); // This should not happen after upsert
        }
        
        while (currentProgress.TimeFrame > timeFrame)
        {
            await StorageService.UpsertNounProgressAsync(userId, nounId, false);
            currentProgress = await StorageService.GetPractiseNounAsync(userId, nounId);
            Assert.NotNull(currentProgress); // This should not happen after upsert
        }
    }

    /// <summary>
    /// Sets up a clean test environment by deleting all progress for a user.
    /// </summary>
    public async Task CleanUserProgressAsync(int userId)
    {
        await StorageService.DeleteAllNounProgressAsync(userId);
    }
}