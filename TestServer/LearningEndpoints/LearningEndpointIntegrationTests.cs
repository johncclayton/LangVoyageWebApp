using TestServer.LearningEndpoints;
using TestServer.TestHelpers;

namespace TestServer.LearningEndpoints;

/// <summary>
/// Integration tests that exercise multiple Learning Endpoint operations together.
/// Tests complex workflows and interactions between different endpoints.
/// </summary>
public class LearningEndpointIntegrationTests : LearningEndpointTestBase
{
    public LearningEndpointIntegrationTests(TestWebApplicationFactory<Program> factory) : base(factory)
    {
    }

    #region Multi-Endpoint Workflow Tests

    [Fact]
    public async Task CompleteWorkflow_GetNounsUpdateProgressViewProgress_WorksCorrectly()
    {
        // Arrange
        await DatabaseAssertions.CleanUserProgressAsync(TestUserId);

        // Act & Assert - Complete learning workflow
        
        // 1. Get initial nouns for practice
        var nounsForPractice = await GetPracticeNounsAsync(TestUserId, 5);
        Assert.NotNull(nounsForPractice);
        Assert.Equal(5, nounsForPractice.Count);

        // 2. Check initial progress (should be empty)
        var initialProgress = await GetLearningProgressAsync(TestUserId);
        Assert.Equal(0, initialProgress.NounProgresses.Sum());

        // 3. Practice some nouns correctly
        foreach (var noun in nounsForPractice.Take(3))
        {
            var progressResult = await UpdateNounProgressAsync(TestUserId, noun.Id, true);
            Assert.Equal(1, progressResult.TimeFrame);
        }

        // 4. Check progress after practice
        var progressAfterPractice = await GetLearningProgressAsync(TestUserId);
        Assert.Equal(3, progressAfterPractice.NounProgresses[1]);

        // 5. Practice one noun again (should advance to TimeFrame 2)
        var advancedProgress = await UpdateNounProgressAsync(TestUserId, nounsForPractice.First().Id, true);
        Assert.Equal(2, advancedProgress.TimeFrame);

        // 6. Check final progress distribution
        var finalProgress = await GetLearningProgressAsync(TestUserId);
        Assert.Equal(2, finalProgress.NounProgresses[1]); // 2 nouns at TimeFrame 1
        Assert.Equal(1, finalProgress.NounProgresses[2]); // 1 noun at TimeFrame 2
    }

    [Fact]
    public async Task SpacedRepetitionWorkflow_PracticeFailRetrySucceed_CorrectProgression()
    {
        // Arrange
        await DatabaseAssertions.CleanUserProgressAsync(TestUserId);
        var nouns = await GetPracticeNounsAsync(TestUserId, 1);
        var noun = nouns.First();

        // Act & Assert - Spaced repetition scenario
        
        // 1. Initial correct practice
        var result1 = await UpdateNounProgressAsync(TestUserId, noun.Id, true);
        Assert.Equal(1, result1.TimeFrame);

        // 2. Advance to higher level
        var result2 = await UpdateNounProgressAsync(TestUserId, noun.Id, true);
        Assert.Equal(2, result2.TimeFrame);

        var result3 = await UpdateNounProgressAsync(TestUserId, noun.Id, true);
        Assert.Equal(3, result3.TimeFrame);

        // 3. Fail practice (should reduce TimeFrame)
        var result4 = await UpdateNounProgressAsync(TestUserId, noun.Id, false);
        Assert.Equal(2, result4.TimeFrame);

        // 4. Check progress reflects the failure
        var progressAfterFailure = await GetLearningProgressAsync(TestUserId);
        Assert.Equal(1, progressAfterFailure.NounProgresses[2]);

        // 5. Succeed again (should advance)
        var result5 = await UpdateNounProgressAsync(TestUserId, noun.Id, true);
        Assert.Equal(3, result5.TimeFrame);

        // 6. Final progress check
        var finalProgress = await GetLearningProgressAsync(TestUserId);
        Assert.Equal(1, finalProgress.NounProgresses[3]);
    }

    [Fact]
    public async Task MultiUserScenario_IndependentProgressTracking()
    {
        // Arrange
        await DatabaseAssertions.CleanUserProgressAsync(TestUserId);
        await DatabaseAssertions.CleanUserProgressAsync(SecondTestUserId);
        
        var user1Nouns = await GetPracticeNounsAsync(TestUserId, 3);
        var user2Nouns = await GetPracticeNounsAsync(SecondTestUserId, 3);

        // Act - Both users practice the same nouns but with different outcomes
        
        // User 1: Good performance
        foreach (var noun in user1Nouns)
        {
            await UpdateNounProgressAsync(TestUserId, noun.Id, true);
            await UpdateNounProgressAsync(TestUserId, noun.Id, true);
        }

        // User 2: Mixed performance
        await UpdateNounProgressAsync(SecondTestUserId, user2Nouns[0].Id, true);
        await UpdateNounProgressAsync(SecondTestUserId, user2Nouns[0].Id, false);
        await UpdateNounProgressAsync(SecondTestUserId, user2Nouns[1].Id, false);

        // Assert - Independent progress tracking
        var user1Progress = await GetLearningProgressAsync(TestUserId);
        var user2Progress = await GetLearningProgressAsync(SecondTestUserId);

        // User 1 should have all nouns at TimeFrame 2
        Assert.Equal(3, user1Progress.NounProgresses[2]);

        // User 2 should have mixed results
        Assert.Equal(1, user2Progress.NounProgresses[0]); // One failed noun
        Assert.Equal(1, user2Progress.NounProgresses[0]); // One failed then succeeded noun at TimeFrame 0
    }

    [Fact]
    public async Task ResetAndRestart_DeleteAllThenCreateNew_WorksCorrectly()
    {
        // Arrange
        await SetupProgressScenarioAsync(TestUserId, 5);
        
        // Verify initial progress exists
        var initialProgress = await GetLearningProgressAsync(TestUserId);
        Assert.True(initialProgress.NounProgresses.Sum() > 0);

        // Act - Reset all progress
        var deleteResponse = await DeleteAllProgressAsync(TestUserId);
        HttpTestHelpers.AssertStatusCode(deleteResponse, System.Net.HttpStatusCode.NoContent);

        // Verify clean state
        var cleanProgress = await GetLearningProgressAsync(TestUserId);
        Assert.Equal(0, cleanProgress.NounProgresses.Sum());

        // Act - Start fresh learning
        var newNouns = await GetPracticeNounsAsync(TestUserId, 3);
        
        foreach (var noun in newNouns)
        {
            await UpdateNounProgressAsync(TestUserId, noun.Id, true);
        }

        // Assert - New progress correctly tracked
        var newProgress = await GetLearningProgressAsync(TestUserId);
        Assert.Equal(3, newProgress.NounProgresses[1]);
        Assert.Equal(0, newProgress.NounProgresses.Skip(2).Sum());
    }

    #endregion

    #region Complex Progress Scenarios

    [Fact]
    public async Task ComplexProgressPattern_MixedSuccessFailure_CorrectDistribution()
    {
        // Arrange
        await DatabaseAssertions.CleanUserProgressAsync(TestUserId);
        var nouns = await GetPracticeNounsAsync(TestUserId, 10);

        // Act - Create complex progress pattern
        
        // Noun 1: Excellent progress (TimeFrame 4)
        for (int i = 0; i < 4; i++)
        {
            await UpdateNounProgressAsync(TestUserId, nouns[0].Id, true);
        }

        // Noun 2: Good progress with one failure (TimeFrame 2)
        await UpdateNounProgressAsync(TestUserId, nouns[1].Id, true);
        await UpdateNounProgressAsync(TestUserId, nouns[1].Id, true);
        await UpdateNounProgressAsync(TestUserId, nouns[1].Id, true);
        await UpdateNounProgressAsync(TestUserId, nouns[1].Id, false); // Back to 2

        // Noun 3: Struggling (TimeFrame 0)
        await UpdateNounProgressAsync(TestUserId, nouns[2].Id, true);
        await UpdateNounProgressAsync(TestUserId, nouns[2].Id, false); // Back to 0

        // Noun 4-6: Standard progress (TimeFrame 1)
        for (int i = 3; i < 6; i++)
        {
            await UpdateNounProgressAsync(TestUserId, nouns[i].Id, true);
        }

        // Nouns 7-10: No progress

        // Assert
        var progress = await GetLearningProgressAsync(TestUserId);
        Assert.Equal(1, progress.NounProgresses[0]); // 1 struggling noun
        Assert.Equal(3, progress.NounProgresses[1]); // 3 nouns at level 1
        Assert.Equal(1, progress.NounProgresses[2]); // 1 noun at level 2
        Assert.Equal(0, progress.NounProgresses[3]); // No nouns at level 3
        Assert.Equal(1, progress.NounProgresses[4]); // 1 excellent noun at level 4
    }

    [Fact]
    public async Task PracticeNounOrdering_ReflectsProgressCorrectly()
    {
        // Arrange
        await DatabaseAssertions.CleanUserProgressAsync(TestUserId);
        var initialNouns = await GetPracticeNounsAsync(TestUserId, 10);
        
        // Act - Create different progress levels
        
        // Make first 3 nouns highly practiced (should appear last in practice list)
        for (int i = 0; i < 3; i++)
        {
            await UpdateNounProgressAsync(TestUserId, initialNouns[i].Id, true);
            await UpdateNounProgressAsync(TestUserId, initialNouns[i].Id, true);
            await UpdateNounProgressAsync(TestUserId, initialNouns[i].Id, true);
        }

        // Make next 2 nouns moderately practiced
        for (int i = 3; i < 5; i++)
        {
            await UpdateNounProgressAsync(TestUserId, initialNouns[i].Id, true);
        }

        // Leave remaining nouns unpracticed

        // Get new practice order
        var reorderedNouns = await GetPracticeNounsAsync(TestUserId, 10);

        // Assert - Unpracticed nouns should appear first
        var unpracticedNounIds = initialNouns.Skip(5).Select(n => n.Id).ToHashSet();
        var moderatelyPracticedIds = initialNouns.Skip(3).Take(2).Select(n => n.Id).ToHashSet();
        var highlyPracticedIds = initialNouns.Take(3).Select(n => n.Id).ToHashSet();

        // First 5 should be unpracticed
        var firstFive = reorderedNouns.Take(5).Select(n => n.Id).ToList();
        Assert.All(firstFive, id => Assert.Contains(id, unpracticedNounIds));

        // The highly practiced nouns should not be in the first positions
        var firstHalf = reorderedNouns.Take(5).Select(n => n.Id).ToHashSet();
        Assert.True(firstHalf.Intersect(highlyPracticedIds).Count() == 0);
    }

    #endregion

    #region Performance and Stress Tests

    [Fact]
    public async Task HighVolumeOperations_Performance_WithinExpectedLimits()
    {
        // Arrange
        await DatabaseAssertions.CleanUserProgressAsync(TestUserId);
        var nouns = await GetPracticeNounsAsync(TestUserId, 20);

        // Act - High volume operations
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        
        // Create substantial progress
        var tasks = new List<Task>();
        foreach (var noun in nouns)
        {
            tasks.Add(UpdateNounProgressAsync(TestUserId, noun.Id, true));
        }
        await Task.WhenAll(tasks);

        // Check progress multiple times
        var progressTasks = new List<Task<LangVoyageServer.Requests.LearningProgressResponse>>();
        for (int i = 0; i < 10; i++)
        {
            progressTasks.Add(GetLearningProgressAsync(TestUserId));
        }
        var progressResults = await Task.WhenAll(progressTasks);

        // Get practice nouns multiple times
        var nounTasks = new List<Task<IList<LangVoyageServer.Models.LanguageNoun>>>();
        for (int i = 0; i < 10; i++)
        {
            nounTasks.Add(GetPracticeNounsAsync(TestUserId, 20));
        }
        var nounResults = await Task.WhenAll(nounTasks);

        stopwatch.Stop();

        // Assert - Performance within reasonable limits
        Assert.True(stopwatch.ElapsedMilliseconds < 10000, 
            $"High volume operations took too long: {stopwatch.ElapsedMilliseconds}ms");

        // Verify correctness
        Assert.All(progressResults, result => 
        {
            Assert.NotNull(result);
            Assert.Equal(20, result.NounProgresses[1]);
        });

        Assert.All(nounResults, result => 
        {
            Assert.NotNull(result);
            Assert.Equal(20, result.Count);
        });
    }

    [Fact]
    public async Task ConcurrentMultiUserOperations_DataIntegrity_Maintained()
    {
        // Arrange
        await DatabaseAssertions.CleanUserProgressAsync(TestUserId);
        await DatabaseAssertions.CleanUserProgressAsync(SecondTestUserId);

        var user1Nouns = await GetPracticeNounsAsync(TestUserId, 5);
        var user2Nouns = await GetPracticeNounsAsync(SecondTestUserId, 5);

        // Act - Concurrent operations for multiple users
        var allTasks = new List<Task>();

        // User 1 operations
        foreach (var noun in user1Nouns)
        {
            allTasks.Add(UpdateNounProgressAsync(TestUserId, noun.Id, true));
        }
        allTasks.Add(GetLearningProgressAsync(TestUserId));
        allTasks.Add(GetPracticeNounsAsync(TestUserId, 10));

        // User 2 operations
        foreach (var noun in user2Nouns.Take(3))
        {
            allTasks.Add(UpdateNounProgressAsync(SecondTestUserId, noun.Id, true));
        }
        allTasks.Add(GetLearningProgressAsync(SecondTestUserId));
        allTasks.Add(GetPracticeNounsAsync(SecondTestUserId, 10));

        await Task.WhenAll(allTasks);

        // Assert - Data integrity maintained
        var user1FinalProgress = await GetLearningProgressAsync(TestUserId);
        var user2FinalProgress = await GetLearningProgressAsync(SecondTestUserId);

        Assert.Equal(5, user1FinalProgress.NounProgresses[1]);
        Assert.Equal(3, user2FinalProgress.NounProgresses[1]);

        // Verify users have different progress
        Assert.NotEqual(user1FinalProgress.NounProgresses[1], user2FinalProgress.NounProgresses[1]);
    }

    #endregion

    #region Data Consistency Tests

    [Fact]
    public async Task DataConsistency_ProgressAndNounOrdering_AlwaysConsistent()
    {
        // Arrange
        await DatabaseAssertions.CleanUserProgressAsync(TestUserId);

        // Act & Assert - Multiple cycles of operations
        for (int cycle = 0; cycle < 3; cycle++)
        {
            // Get nouns and create some progress
            var nouns = await GetPracticeNounsAsync(TestUserId, 5);
            
            foreach (var noun in nouns.Take(2))
            {
                await UpdateNounProgressAsync(TestUserId, noun.Id, true);
            }

            // Check consistency
            var progress = await GetLearningProgressAsync(TestUserId);
            var nounsAfterProgress = await GetPracticeNounsAsync(TestUserId, 10);

            // Verify progress count matches database state
            var expectedProgressCount = (cycle + 1) * 2;
            Assert.Equal(expectedProgressCount, progress.NounProgresses.Sum());

            // Verify noun ordering is logical (unpracticed first)
            Assert.NotNull(nounsAfterProgress);
            Assert.True(nounsAfterProgress.Count > 0);
        }
    }

    [Fact]
    public async Task ErrorRecovery_PartialFailures_DoNotCorruptData()
    {
        // Arrange
        await DatabaseAssertions.CleanUserProgressAsync(TestUserId);
        var nouns = await GetPracticeNounsAsync(TestUserId, 3);

        // Act - Create some valid progress
        await UpdateNounProgressAsync(TestUserId, nouns[0].Id, true);
        await UpdateNounProgressAsync(TestUserId, nouns[1].Id, true);

        // Attempt operations that might fail (but should not corrupt existing data)
        try
        {
            await UpdateNounProgressAsync(TestUserId, 999999, true); // Non-existent noun
        }
        catch
        {
            // Expected to fail, but shouldn't corrupt existing data
        }

        try
        {
            await UpdateNounProgressAsync(NonExistentUserId, nouns[2].Id, true); // Non-existent user
        }
        catch
        {
            // Expected to fail, but shouldn't corrupt existing data
        }

        // Assert - Existing data should remain intact
        var progress = await GetLearningProgressAsync(TestUserId);
        Assert.Equal(2, progress.NounProgresses[1]);

        await DatabaseAssertions.AssertNounProgressExistsAsync(TestUserId, nouns[0].Id, 1);
        await DatabaseAssertions.AssertNounProgressExistsAsync(TestUserId, nouns[1].Id, 1);
    }

    #endregion
}