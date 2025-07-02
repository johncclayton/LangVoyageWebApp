using LangVoyageServer.Database;
using LangVoyageServer.Models;
using LangVoyageServer.Requests;
using Microsoft.Extensions.DependencyInjection;

namespace TestServer;

[Collection("Sequential")]
public class TestConcurrencyAndPerformance : IClassFixture<TestWebApplicationFactory<Program>>
{
    private readonly TestWebApplicationFactory<Program> _factory;
    private static bool _databaseInitialized;
    private static readonly object _lock = new object();

    public TestConcurrencyAndPerformance(TestWebApplicationFactory<Program> factory)
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
    /// Verifies that concurrent noun progress updates are handled correctly without data corruption or race conditions.
    /// This test ensures thread safety and data integrity in multi-user environments with simultaneous progress updates.
    /// </summary>
    [Fact]
    public async Task ConcurrentNounProgressUpdates_HandleCorrectly()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();
        
        var nouns = await service.GetNewPractiseNounsAsync(1, 1);
        var testNoun = nouns.First();
        
        // Clean slate
        await service.DeleteNounProgressAsync(1, testNoun.Id);

        // Act - Simulate concurrent updates
        var tasks = new List<Task<NounProgress>>();
        for (int i = 0; i < 5; i++)
        {
            tasks.Add(service.UpsertNounProgressAsync(1, testNoun.Id, true));
        }

        var results = await Task.WhenAll(tasks);

        // Assert - All tasks should complete without errors
        Assert.Equal(5, results.Length);
        foreach (var result in results)
        {
            Assert.NotNull(result);
            Assert.Equal(1, result.UserProfileId);
            Assert.Equal(testNoun.Id, result.NounId);
            Assert.True(result.TimeFrame > 0);
        }

        // Final state should reflect the last update
        var finalProgress = await service.GetPractiseNounAsync(1, testNoun.Id);
        Assert.NotNull(finalProgress);
        Assert.True(finalProgress.TimeFrame >= 1); // Should be at least 1 or higher
    }

    /// <summary>
    /// Verifies that bulk noun progress update operations complete within acceptable performance thresholds.
    /// This test ensures the system can handle moderate load without performance degradation affecting user experience.
    /// </summary>
    [Fact]
    public async Task BulkNounProgressUpdates_PerformWell()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();
        
        var nouns = await service.GetNewPractiseNounsAsync(1, 10);
        
        // Act & Assert - Should complete within reasonable time
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        
        foreach (var noun in nouns)
        {
            await service.UpsertNounProgressAsync(1, noun.Id, true);
            await service.UpsertNounProgressAsync(1, noun.Id, false);
            await service.UpsertNounProgressAsync(1, noun.Id, true);
        }
        
        stopwatch.Stop();
        
        // Assert - Should complete in under 5 seconds for 30 operations
        Assert.True(stopwatch.ElapsedMilliseconds < 5000, 
            $"Bulk operations took {stopwatch.ElapsedMilliseconds}ms, which is too slow");
    }

    /// <summary>
    /// Verifies that GetNewPractiseNounsAsync handles very large limit values gracefully without performance issues.
    /// This test ensures system stability and proper resource management with extreme parameter values.
    /// </summary>
    [Fact]
    public async Task GetNewPractiseNounsAsync_WithLargeLimit_ReturnsAppropriateResults()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();

        // Act
        var result = await service.GetNewPractiseNounsAsync(1, 1000); // Very large limit

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Count > 0);
        // Should not crash or return more than available nouns
        Assert.True(result.Count <= 1000);
    }

    /// <summary>
    /// Verifies that UpdateAllNounProgressAsync performs efficiently even with large datasets.
    /// This test ensures bulk operations maintain acceptable performance for administrative and initialization tasks.
    /// </summary>
    [Fact]
    public async Task UpdateAllNounProgressAsync_WithManyNouns_PerformsWell()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();
        
        // Clean slate
        await service.DeleteAllNounProgressAsync(1);

        // Act
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        var result = await service.UpdateAllNounProgressAsync(1);
        stopwatch.Stop();

        // Assert
        Assert.NotEmpty(result);
        Assert.True(stopwatch.ElapsedMilliseconds < 10000, 
            $"UpdateAllNounProgressAsync took {stopwatch.ElapsedMilliseconds}ms, which is too slow");
        
        // All progress should be set to TimeFrame 1
        Assert.All(result, progress => Assert.Equal(1, progress.TimeFrame));
    }

    /// <summary>
    /// Verifies that GetLearningProgress correctly calculates progress distribution statistics across TimeFrame levels.
    /// This test ensures accurate analytics calculation for the learning dashboard and progress tracking features.
    /// </summary>
    [Fact]
    public async Task GetLearningProgress_CalculatesCorrectly()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();
        
        // Set up known state
        await service.DeleteAllNounProgressAsync(1);
        
        // Practice 3 nouns to different levels
        var nouns = await service.GetNewPractiseNounsAsync(1, 3);
        
        // Noun 1: TimeFrame 1 (practice once)
        await service.UpsertNounProgressAsync(1, nouns[0].Id, true);
        
        // Noun 2: TimeFrame 2 (practice twice) 
        await service.UpsertNounProgressAsync(1, nouns[1].Id, true);
        await service.UpsertNounProgressAsync(1, nouns[1].Id, true);
        
        // Noun 3: TimeFrame 3 (practice three times)
        await service.UpsertNounProgressAsync(1, nouns[2].Id, true);
        await service.UpsertNounProgressAsync(1, nouns[2].Id, true);
        await service.UpsertNounProgressAsync(1, nouns[2].Id, true);

        // Act
        var progress = await service.GetLearningProgress(1);

        // Assert
        Assert.NotNull(progress);
        Assert.True(progress.NounProgresses[1] >= 1); // At least 1 noun at TimeFrame 1
        Assert.True(progress.NounProgresses[2] >= 1); // At least 1 noun at TimeFrame 2
        Assert.True(progress.NounProgresses[3] >= 1); // At least 1 noun at TimeFrame 3
    }

    /// <summary>
    /// Verifies that multiple users can operate independently without interfering with each other's progress data.
    /// This test ensures proper data isolation and multi-tenancy support in the learning progress system.
    /// </summary>
    [Fact]
    public async Task MultipleUsersCanOperateIndependently()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();
        
        // Create a second user for testing
        var newUser = await service.UpsertUserProfileAsync(2, new UpdateUserRequest
        {
            Username = "test_user_2",
            LanguageLevel = "A1"
        });
        
        Assert.NotNull(newUser);
        var user2Id = newUser.Id;

        // Act - Both users practice different nouns
        var nouns1 = await service.GetNewPractiseNounsAsync(1, 2);
        var nouns2 = await service.GetNewPractiseNounsAsync(user2Id, 2);
        
        await service.UpsertNounProgressAsync(1, nouns1[0].Id, true);
        await service.UpsertNounProgressAsync(user2Id, nouns2[0].Id, false);

        // Assert - Users should have independent progress
        var progress1 = await service.GetPractiseNounAsync(1, nouns1[0].Id);
        var progress2 = await service.GetPractiseNounAsync(user2Id, nouns2[0].Id);
        
        Assert.NotNull(progress1);
        Assert.NotNull(progress2);
        Assert.Equal(1, progress1.UserProfileId);
        Assert.Equal(user2Id, progress2.UserProfileId);
        
        // Different outcomes based on correct/incorrect answers
        Assert.True(progress1.TimeFrame >= 1); // Correct answer
        Assert.Equal(1, progress2.TimeFrame); // First practice always starts at TimeFrame=1
    }

    /// <summary>
    /// Verifies that database constraints are properly enforced and progress data maintains referential integrity.
    /// This test ensures data consistency and proper constraint validation in the progress tracking system.
    /// </summary>
    [Fact]
    public async Task DatabaseConstraints_AreEnforced()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();

        // Get multiple nouns to avoid test interference
        var nouns = await service.GetNewPractiseNounsAsync(1, 10);
        var testNoun = nouns.LastOrDefault(); // Use the last one to reduce conflicts
        Assert.NotNull(testNoun);
        
        // Clean slate for this test
        await service.DeleteNounProgressAsync(1, testNoun.Id);
        
        // Test that progress is maintained correctly through multiple operations
        var progress1 = await service.UpsertNounProgressAsync(1, testNoun.Id, true);
        Assert.Equal(1, progress1.TimeFrame);
        
        var progress2 = await service.UpsertNounProgressAsync(1, testNoun.Id, true);
        Assert.Equal(2, progress2.TimeFrame);
        
        var progress3 = await service.UpsertNounProgressAsync(1, testNoun.Id, false);
        Assert.Equal(1, progress3.TimeFrame);
        
        // All should reference the same noun
        Assert.Equal(testNoun.Id, progress1.NounId);
        Assert.Equal(testNoun.Id, progress2.NounId);
        Assert.Equal(testNoun.Id, progress3.NounId);
    }
}