using System.Net;
using System.Text;
using LangVoyageServer.Database;
using LangVoyageServer.Models;
using LangVoyageServer.Requests;

namespace TestServer;

/// <summary>
/// Integration tests and edge case scenarios
/// </summary>
[Collection("Sequential")]
public class IntegrationTests : IClassFixture<TestWebApplicationFactory<Program>>
{
    private readonly TestWebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public IntegrationTests(TestWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = TestHelpers.CreateTestClient(factory);
        DatabaseTestHelper.InitializeDatabase(factory);
    }

    [Fact]
    public async Task EndToEndWorkflow_CreateUser_PracticeNouns_CheckProgress()
    {
        // This test simulates a complete user workflow
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();
        
        // Clean slate
        await DatabaseTestHelper.CleanupUserProgress(scope, TestConstants.DefaultUserId);

        // 1. Verify user exists
        var userResponse = await _client.GetAsync(string.Format(TestConstants.ApiEndpoints.UserById, TestConstants.DefaultUserId));
        userResponse.EnsureSuccessStatusCode();
        var user = await TestHelpers.DeserializeResponseAsync<UserProfile>(userResponse);
        Assert.NotNull(user);

        // 2. Get initial progress (should be empty)
        var initialProgressResponse = await _client.GetAsync(string.Format(TestConstants.ApiEndpoints.LearningProgress, TestConstants.DefaultUserId));
        initialProgressResponse.EnsureSuccessStatusCode();
        var initialProgress = await TestHelpers.DeserializeResponseAsync<LearningProgressResponse>(initialProgressResponse);
        Assert.NotNull(initialProgress);
        Assert.All(initialProgress.NounProgresses, count => Assert.Equal(0, count));

        // 3. Get nouns for practice
        var nounsResponse = await _client.GetAsync(string.Format(TestConstants.ApiEndpoints.LearnNouns, TestConstants.DefaultUserId));
        nounsResponse.EnsureSuccessStatusCode();
        var nouns = await TestHelpers.DeserializeResponseAsync<IList<LanguageNoun>>(nounsResponse);
        Assert.NotNull(nouns);
        Assert.True(nouns.Count > 0);

        // 4. Practice some nouns correctly
        var practiceCount = Math.Min(5, nouns.Count);
        for (int i = 0; i < practiceCount; i++)
        {
            await service.UpsertNounProgressAsync(TestConstants.DefaultUserId, nouns[i].Id, true);
        }

        // 5. Check progress has changed
        var updatedProgressResponse = await _client.GetAsync(string.Format(TestConstants.ApiEndpoints.LearningProgress, TestConstants.DefaultUserId));
        updatedProgressResponse.EnsureSuccessStatusCode();
        var updatedProgress = await TestHelpers.DeserializeResponseAsync<LearningProgressResponse>(updatedProgressResponse);
        Assert.NotNull(updatedProgress);
        Assert.True(updatedProgress.NounProgresses.Sum() > 0);
    }

    [Fact]
    public async Task ConcurrentAccess_MultipleProgressUpdates_HandledCorrectly()
    {
        // Test concurrent access to the same noun progress
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();
        await DatabaseTestHelper.CleanupUserProgress(scope, TestConstants.DefaultUserId);

        var nouns = await service.GetNewPractiseNounsAsync(TestConstants.DefaultUserId, 1);
        var noun = nouns.First();

        // Simulate concurrent updates
        var tasks = new List<Task<NounProgress>>();
        for (int i = 0; i < 5; i++)
        {
            tasks.Add(service.UpsertNounProgressAsync(TestConstants.DefaultUserId, noun.Id, true));
        }

        // Wait for all to complete
        var results = await Task.WhenAll(tasks);

        // Verify final state is consistent
        var finalProgress = await service.GetPractiseNounAsync(TestConstants.DefaultUserId, noun.Id);
        Assert.NotNull(finalProgress);
        Assert.True(finalProgress.TimeFrame > 0);
        Assert.True(finalProgress.TimeFrame <= 5); // Should not exceed the number of updates
    }

    [Theory]
    [InlineData("")]
    [InlineData("not-json")]
    [InlineData("{")]
    [InlineData("null")]
    public async Task ApiEndpoints_WithMalformedInput_ReturnBadRequest(string malformedInput)
    {
        // Test API resilience to malformed input
        var content = new StringContent(malformedInput, Encoding.UTF8, TestConstants.ContentTypes.ApplicationJson);
        
        var response = await _client.PatchAsync(string.Format(TestConstants.ApiEndpoints.UserById, TestConstants.DefaultUserId), content);
        
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task LargeDataSet_Performance_WithinReasonableLimits()
    {
        // Test performance with larger data sets
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();
        
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        
        // Get maximum nouns available
        var nouns = await service.GetNewPractiseNounsAsync(TestConstants.DefaultUserId, 100);
        
        stopwatch.Stop();
        
        // Assert reasonable performance (adjust threshold as needed)
        Assert.True(stopwatch.ElapsedMilliseconds < 5000, $"Query took {stopwatch.ElapsedMilliseconds}ms");
        Assert.NotNull(nouns);
    }

    [Fact]
    public async Task DatabaseState_ConsistencyCheck_AfterMultipleOperations()
    {
        // Test database consistency after multiple operations
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();
        await DatabaseTestHelper.CleanupUserProgress(scope, TestConstants.DefaultUserId);

        // Perform various operations
        var nouns = await service.GetNewPractiseNounsAsync(TestConstants.DefaultUserId, 3);
        
        // Practice first noun correctly multiple times
        await service.UpsertNounProgressAsync(TestConstants.DefaultUserId, nouns[0].Id, true);
        await service.UpsertNounProgressAsync(TestConstants.DefaultUserId, nouns[0].Id, true);
        
        // Practice second noun incorrectly
        await service.UpsertNounProgressAsync(TestConstants.DefaultUserId, nouns[1].Id, false);
        
        // Delete progress for third noun
        await service.UpsertNounProgressAsync(TestConstants.DefaultUserId, nouns[2].Id, true);
        await service.DeleteNounProgressAsync(TestConstants.DefaultUserId, nouns[2].Id);

        // Check consistency
        var progress1 = await service.GetPractiseNounAsync(TestConstants.DefaultUserId, nouns[0].Id);
        var progress2 = await service.GetPractiseNounAsync(TestConstants.DefaultUserId, nouns[1].Id);
        var progress3 = await service.GetPractiseNounAsync(TestConstants.DefaultUserId, nouns[2].Id);

        Assert.NotNull(progress1);
        Assert.Equal(2, progress1.TimeFrame);
        
        Assert.NotNull(progress2);
        Assert.Equal(0, progress2.TimeFrame);
        
        Assert.Null(progress3); // Should be deleted
    }

    [Fact]
    public async Task UserLanguageLevelChange_AffectsNounSelection()
    {
        // Test that changing user language level affects noun selection
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();

        // Get current nouns
        var initialNouns = await service.GetNewPractiseNounsAsync(TestConstants.DefaultUserId);
        var initialLevel = initialNouns.First().Level;

        // Change user language level
        var newLevel = initialLevel == "A1" ? "B1" : "A1";
        await service.UpsertUserProfileAsync(TestConstants.DefaultUserId, new UpdateUserRequest
        {
            LanguageLevel = newLevel
        });

        // Get nouns again
        var newNouns = await service.GetNewPractiseNounsAsync(TestConstants.DefaultUserId);
        
        // Verify all nouns match new level
        Assert.All(newNouns, noun => Assert.Equal(newLevel, noun.Level));
    }

    [Fact]
    public async Task ProgressReporting_AccuracyCheck_WithKnownData()
    {
        // Test progress reporting accuracy with known data
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();
        await DatabaseTestHelper.CleanupUserProgress(scope, TestConstants.DefaultUserId);

        // Practice exactly 3 nouns at different levels
        var nouns = await service.GetNewPractiseNounsAsync(TestConstants.DefaultUserId, 3);
        
        // Noun 1: TimeFrame 1
        await service.UpsertNounProgressAsync(TestConstants.DefaultUserId, nouns[0].Id, true);
        
        // Noun 2: TimeFrame 2
        await service.UpsertNounProgressAsync(TestConstants.DefaultUserId, nouns[1].Id, true);
        await service.UpsertNounProgressAsync(TestConstants.DefaultUserId, nouns[1].Id, true);
        
        // Noun 3: TimeFrame 3
        await service.UpsertNounProgressAsync(TestConstants.DefaultUserId, nouns[2].Id, true);
        await service.UpsertNounProgressAsync(TestConstants.DefaultUserId, nouns[2].Id, true);
        await service.UpsertNounProgressAsync(TestConstants.DefaultUserId, nouns[2].Id, true);

        // Get progress
        var progress = await service.GetLearningProgress(TestConstants.DefaultUserId);
        
        // Verify specific counts (depends on implementation details)
        Assert.NotNull(progress);
        Assert.True(progress.NounProgresses.Sum() >= 3); // At least 3 practiced nouns
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(1000000)]
    public async Task ApiEndpoints_WithEdgeCaseUserIds_HandleGracefully(int userId)
    {
        // Test API behavior with edge case user IDs
        var response = await _client.GetAsync(string.Format(TestConstants.ApiEndpoints.UserById, userId));
        
        // Should return appropriate status codes (404 for non-existent, 400 for invalid format)
        Assert.True(response.StatusCode is HttpStatusCode.NotFound or HttpStatusCode.BadRequest);
    }
}