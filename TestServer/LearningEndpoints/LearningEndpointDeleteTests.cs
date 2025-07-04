using System.Net;
using System.Net;
using TestServer.LearningEndpoints;
using TestServer.TestHelpers;

namespace TestServer.LearningEndpoints;

/// <summary>
/// Comprehensive tests for the Learning Endpoint DELETE operations.
/// Tests the delete all progress endpoint (/learn/v1/{userId}/noun).
/// </summary>
public class LearningEndpointDeleteTests : LearningEndpointTestBase
{
    public LearningEndpointDeleteTests(TestWebApplicationFactory<Program> factory) : base(factory)
    {
    }

    #region DELETE /learn/v1/{userId}/noun Basic Tests

    [Fact]
    public async Task DeleteAllProgress_WithExistingProgress_RemovesAllProgress()
    {
        // Arrange
        await SetupProgressScenarioAsync(TestUserId, 5);
        
        // Verify progress exists before deletion
        var progressBefore = await GetLearningProgressAsync(TestUserId);
        Assert.True(progressBefore.NounProgresses.Sum() > 0);

        // Act
        var response = await DeleteAllProgressAsync(TestUserId);

        // Assert
        HttpTestHelpers.AssertStatusCode(response, HttpStatusCode.NoContent);
        await DatabaseAssertions.AssertUserHasNoProgressAsync(TestUserId);
        
        // Verify progress is empty after deletion
        var progressAfter = await GetLearningProgressAsync(TestUserId);
        Assert.Equal(0, progressAfter.NounProgresses.Sum());
    }

    [Fact]
    public async Task DeleteAllProgress_WithNoExistingProgress_SucceedsWithNoContent()
    {
        // Arrange
        await DatabaseAssertions.CleanUserProgressAsync(TestUserId);
        
        // Verify no progress exists
        await DatabaseAssertions.AssertUserHasNoProgressAsync(TestUserId);

        // Act
        var response = await DeleteAllProgressAsync(TestUserId);

        // Assert
        HttpTestHelpers.AssertStatusCode(response, HttpStatusCode.NoContent);
        await DatabaseAssertions.AssertUserHasNoProgressAsync(TestUserId);
    }

    [Fact]
    public async Task DeleteAllProgress_AfterDeletion_CanCreateNewProgress()
    {
        // Arrange
        await SetupProgressScenarioAsync(TestUserId, 3);
        
        // Act - Delete all progress
        var deleteResponse = await DeleteAllProgressAsync(TestUserId);
        HttpTestHelpers.AssertStatusCode(deleteResponse, HttpStatusCode.NoContent);

        // Act - Create new progress
        var nouns = await GetPracticeNounsAsync(TestUserId, 1);
        var newProgress = await UpdateNounProgressAsync(TestUserId, nouns.First().Id, true);

        // Assert
        Assert.NotNull(newProgress);
        Assert.Equal(1, newProgress.TimeFrame);
        await DatabaseAssertions.AssertNounProgressExistsAsync(TestUserId, nouns.First().Id, 1);
    }

    [Fact]
    public async Task DeleteAllProgress_OnlyAffectsTargetUser()
    {
        // Arrange
        await SetupProgressScenarioAsync(TestUserId, 3);
        await SetupProgressScenarioAsync(SecondTestUserId, 3);
        
        // Verify both users have progress
        var user1ProgressBefore = await GetLearningProgressAsync(TestUserId);
        var user2ProgressBefore = await GetLearningProgressAsync(SecondTestUserId);
        Assert.True(user1ProgressBefore.NounProgresses.Sum() > 0);
        Assert.True(user2ProgressBefore.NounProgresses.Sum() > 0);

        // Act - Delete only first user's progress
        var response = await DeleteAllProgressAsync(TestUserId);

        // Assert
        HttpTestHelpers.AssertStatusCode(response, HttpStatusCode.NoContent);
        await DatabaseAssertions.AssertUserHasNoProgressAsync(TestUserId);
        
        // Second user's progress should be unaffected
        var user2ProgressAfter = await GetLearningProgressAsync(SecondTestUserId);
        Assert.True(user2ProgressAfter.NounProgresses.Sum() > 0);
    }

    #endregion

    #region DELETE /learn/v1/{userId}/noun HTTP Status Code Tests

    [Fact]
    public async Task DeleteAllProgress_ValidUser_ReturnsNoContent()
    {
        // Arrange
        await SetupProgressScenarioAsync(TestUserId, 2);

        // Act
        var response = await DeleteAllProgressAsync(TestUserId);

        // Assert
        HttpTestHelpers.AssertStatusCode(response, HttpStatusCode.NoContent);
        Assert.Equal(string.Empty, await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task DeleteAllProgress_ValidUserNoProgress_ReturnsNoContent()
    {
        // Arrange
        await DatabaseAssertions.CleanUserProgressAsync(TestUserId);

        // Act
        var response = await DeleteAllProgressAsync(TestUserId);

        // Assert
        HttpTestHelpers.AssertStatusCode(response, HttpStatusCode.NoContent);
        Assert.Equal(string.Empty, await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task DeleteAllProgress_InvalidUser_ReturnsNoContent()
    {
        // Note: The delete operation might return NoContent even for non-existent users
        // This is common behavior for DELETE operations to be idempotent
        
        // Act
        var response = await DeleteAllProgressAsync(NonExistentUserId);

        // Assert
        HttpTestHelpers.AssertStatusCode(response, HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task DeleteAllProgress_NegativeUserId_ReturnsNoContent()
    {
        // Act
        var response = await DeleteAllProgressAsync(-1);

        // Assert
        HttpTestHelpers.AssertStatusCode(response, HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task DeleteAllProgress_ZeroUserId_ReturnsNoContent()
    {
        // Act
        var response = await DeleteAllProgressAsync(0);

        // Assert
        HttpTestHelpers.AssertStatusCode(response, HttpStatusCode.NoContent);
    }

    #endregion

    #region DELETE /learn/v1/{userId}/noun Edge Cases and Error Handling

    [Fact]
    public async Task DeleteAllProgress_LargeAmountOfProgress_HandlesEfficiently()
    {
        // Arrange - Create substantial progress data
        await DatabaseAssertions.CleanUserProgressAsync(TestUserId);
        var nouns = await GetPracticeNounsAsync(TestUserId, 15);
        
        // Create varying levels of progress for each noun
        foreach (var noun in nouns)
        {
            var progressCount = new Random().Next(1, 5);
            for (int i = 0; i < progressCount; i++)
            {
                await UpdateNounProgressAsync(TestUserId, noun.Id, true);
            }
        }

        // Verify substantial progress exists
        var progressBefore = await GetLearningProgressAsync(TestUserId);
        Assert.True(progressBefore.NounProgresses.Sum() >= 15);

        // Act
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        var response = await DeleteAllProgressAsync(TestUserId);
        stopwatch.Stop();

        // Assert
        HttpTestHelpers.AssertStatusCode(response, HttpStatusCode.NoContent);
        await DatabaseAssertions.AssertUserHasNoProgressAsync(TestUserId);
        
        // Performance assertion
        Assert.True(stopwatch.ElapsedMilliseconds < 2000, 
            $"Delete operation took too long: {stopwatch.ElapsedMilliseconds}ms");
    }

    [Fact]
    public async Task DeleteAllProgress_MultipleConcurrentDeletes_HandlesSafely()
    {
        // Arrange
        await SetupProgressScenarioAsync(TestUserId, 5);

        // Act - Multiple concurrent delete requests
        var tasks = new List<Task<HttpResponseMessage>>();
        for (int i = 0; i < 3; i++)
        {
            tasks.Add(DeleteAllProgressAsync(TestUserId));
        }

        var responses = await Task.WhenAll(tasks);

        // Assert - All should succeed
        Assert.All(responses, response => 
            HttpTestHelpers.AssertStatusCode(response, HttpStatusCode.NoContent));
        
        await DatabaseAssertions.AssertUserHasNoProgressAsync(TestUserId);
    }

    [Fact]
    public async Task DeleteAllProgress_DuringActiveProgress_DoesNotCorruptState()
    {
        // Arrange
        await SetupProgressScenarioAsync(TestUserId, 3);

        // Act - Simulate concurrent progress update and delete
        var nouns = await GetPracticeNounsAsync(TestUserId, 1);
        var updateTask = UpdateNounProgressAsync(TestUserId, nouns.First().Id, true);
        var deleteTask = DeleteAllProgressAsync(TestUserId);

        await Task.WhenAll(updateTask, deleteTask);

        // Assert - State should be consistent (either all deleted or new progress exists)
        var progressAfter = await GetLearningProgressAsync(TestUserId);
        var totalProgress = progressAfter.NounProgresses.Sum();
        
        // Should either be completely clean (0) or have the one new progress (1)
        Assert.True(totalProgress == 0 || totalProgress == 1);
    }

    #endregion

    #region DELETE /learn/v1/{userId}/noun Idempotency Tests

    [Fact]
    public async Task DeleteAllProgress_IdempotentOperation_MultipleCallsSameResult()
    {
        // Arrange
        await SetupProgressScenarioAsync(TestUserId, 3);
        
        // Verify initial progress
        var initialProgress = await GetLearningProgressAsync(TestUserId);
        Assert.True(initialProgress.NounProgresses.Sum() > 0);

        // Act - First delete
        var response1 = await DeleteAllProgressAsync(TestUserId);
        HttpTestHelpers.AssertStatusCode(response1, HttpStatusCode.NoContent);
        await DatabaseAssertions.AssertUserHasNoProgressAsync(TestUserId);

        // Act - Second delete (should be idempotent)
        var response2 = await DeleteAllProgressAsync(TestUserId);
        HttpTestHelpers.AssertStatusCode(response2, HttpStatusCode.NoContent);
        await DatabaseAssertions.AssertUserHasNoProgressAsync(TestUserId);

        // Act - Third delete (should still be idempotent)
        var response3 = await DeleteAllProgressAsync(TestUserId);
        HttpTestHelpers.AssertStatusCode(response3, HttpStatusCode.NoContent);
        await DatabaseAssertions.AssertUserHasNoProgressAsync(TestUserId);
    }

    [Fact]
    public async Task DeleteAllProgress_AfterMultipleDeletes_CanStillCreateProgress()
    {
        // Arrange
        await SetupProgressScenarioAsync(TestUserId, 2);

        // Act - Multiple delete operations
        await DeleteAllProgressAsync(TestUserId);
        await DeleteAllProgressAsync(TestUserId);
        await DeleteAllProgressAsync(TestUserId);

        // Act - Create new progress after multiple deletes
        var nouns = await GetPracticeNounsAsync(TestUserId, 1);
        var newProgress = await UpdateNounProgressAsync(TestUserId, nouns.First().Id, true);

        // Assert
        Assert.NotNull(newProgress);
        Assert.Equal(1, newProgress.TimeFrame);
        await DatabaseAssertions.AssertNounProgressExistsAsync(TestUserId, nouns.First().Id, 1);
    }

    #endregion

    #region DELETE /learn/v1/{userId}/noun Integration Tests

    [Fact]
    public async Task DeleteAllProgress_FullWorkflow_CreateUpdateDelete()
    {
        // Arrange
        await DatabaseAssertions.CleanUserProgressAsync(TestUserId);
        var nouns = await GetPracticeNounsAsync(TestUserId, 3);

        // Act & Assert - Full workflow
        
        // 1. Create initial progress
        foreach (var noun in nouns.Take(2))
        {
            await UpdateNounProgressAsync(TestUserId, noun.Id, true);
        }
        
        var progressAfterCreate = await GetLearningProgressAsync(TestUserId);
        Assert.Equal(2, progressAfterCreate.NounProgresses.Sum());

        // 2. Update existing progress
        await UpdateNounProgressAsync(TestUserId, nouns.First().Id, true);
        
        var progressAfterUpdate = await GetLearningProgressAsync(TestUserId);
        Assert.Equal(2, progressAfterUpdate.NounProgresses.Sum());
        Assert.Equal(1, progressAfterUpdate.NounProgresses[1]); // One at level 1
        Assert.Equal(1, progressAfterUpdate.NounProgresses[2]); // One at level 2

        // 3. Delete all progress
        var deleteResponse = await DeleteAllProgressAsync(TestUserId);
        HttpTestHelpers.AssertStatusCode(deleteResponse, HttpStatusCode.NoContent);
        
        var progressAfterDelete = await GetLearningProgressAsync(TestUserId);
        Assert.Equal(0, progressAfterDelete.NounProgresses.Sum());

        // 4. Verify can create new progress
        await UpdateNounProgressAsync(TestUserId, nouns.Last().Id, true);
        
        var finalProgress = await GetLearningProgressAsync(TestUserId);
        Assert.Equal(1, finalProgress.NounProgresses.Sum());
        Assert.Equal(1, finalProgress.NounProgresses[1]);
    }

    [Fact]
    public async Task DeleteAllProgress_ImpactOnPracticeNounsOrdering_ResetsToDefault()
    {
        // Arrange
        await DatabaseAssertions.CleanUserProgressAsync(TestUserId);
        var initialNouns = await GetPracticeNounsAsync(TestUserId, 10);
        
        // Create progress to change ordering
        for (int i = 0; i < 5; i++)
        {
            await UpdateNounProgressAsync(TestUserId, initialNouns[i].Id, true);
            await UpdateNounProgressAsync(TestUserId, initialNouns[i].Id, true);
        }
        
        var nounsAfterProgress = await GetPracticeNounsAsync(TestUserId, 10);
        
        // Verify ordering changed (nouns with no progress should be first)
        var nounsWithoutProgress = initialNouns.Skip(5).Take(5).Select(n => n.Id).ToList();
        var firstFiveAfterProgress = nounsAfterProgress.Take(5).Select(n => n.Id).ToList();
        
        // Act - Delete all progress
        await DeleteAllProgressAsync(TestUserId);
        
        // Get nouns after deletion
        var nounsAfterDeletion = await GetPracticeNounsAsync(TestUserId, 10);

        // Assert - Ordering should reset to original (or at least consistent)
        Assert.NotNull(nounsAfterDeletion);
        Assert.Equal(10, nounsAfterDeletion.Count);
        
        // The ordering should be consistent with initial state (no progress)
        for (int i = 0; i < 10; i++)
        {
            Assert.Equal(initialNouns[i].Id, nounsAfterDeletion[i].Id);
        }
    }

    #endregion
}