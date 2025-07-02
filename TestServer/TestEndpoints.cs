using System.Net;
using System.Text;
using System.Text.Json;
using LangVoyageServer.Database;
using LangVoyageServer.Models;
using LangVoyageServer.Requests;

namespace TestServer;

/// <summary>
/// Legacy test endpoints - refactored to use new helper classes and improved structure
/// This class maintains backward compatibility while improving test quality
/// </summary>
[Collection("Sequential")]
public class TestEndpoints : IClassFixture<TestWebApplicationFactory<Program>>
{
    private readonly TestWebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public TestEndpoints(TestWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = TestHelpers.CreateTestClient(factory);
        DatabaseTestHelper.InitializeDatabase(factory);
    }

    [Fact]
    public async Task TestLearning_UserGetsTheRightLevelNouns()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        await DatabaseTestHelper.CleanupUserProgress(scope, TestConstants.DefaultUserId);

        // Act - Get user profile
        var userResponse = await _client.GetAsync(string.Format(TestConstants.ApiEndpoints.UserById, TestConstants.DefaultUserId));
        userResponse.EnsureSuccessStatusCode();
        var user = await TestHelpers.DeserializeResponseAsync<UserProfile>(userResponse);

        // Act - Get nouns for practice
        var response = await _client.GetAsync(string.Format(TestConstants.ApiEndpoints.LearnNouns, TestConstants.DefaultUserId));
        response.EnsureSuccessStatusCode();
        var nouns = await TestHelpers.DeserializeResponseAsync<IList<LanguageNoun>>(response);

        // Assert
        TestHelpers.AssertUserProfile(user, TestConstants.DefaultUserId);
        TestHelpers.AssertLanguageNouns(nouns, TestConstants.DefaultNounLimit, user?.LanguageLevel);
    }

    [Fact]
    public async Task TestLearning_NounProgressBoundaryTests()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();
        await DatabaseTestHelper.CleanupUserProgress(scope, TestConstants.DefaultUserId);

        // Get a noun and build up its progress
        var nouns = await service.GetNewPractiseNounsAsync(TestConstants.DefaultUserId, 1);
        var noun = nouns.First();

        // Practice correctly twice to build up TimeFrame
        await service.UpsertNounProgressAsync(TestConstants.DefaultUserId, noun.Id, true);
        await service.UpsertNounProgressAsync(TestConstants.DefaultUserId, noun.Id, true);

        var nounProgress = await service.GetPractiseNounAsync(TestConstants.DefaultUserId, noun.Id);
        Assert.NotNull(nounProgress);

        // Act - Practice incorrectly to reduce TimeFrame to zero
        var initialTimeFrame = nounProgress.TimeFrame;
        for (int index = 0; index < initialTimeFrame; index++)
        {
            await service.UpsertNounProgressAsync(TestConstants.DefaultUserId, nounProgress.NounId, false);
        }

        // Assert - Should be at 0
        var practiceNoun = await service.GetPractiseNounAsync(TestConstants.DefaultUserId, nounProgress.NounId);
        Assert.NotNull(practiceNoun);
        Assert.Equal(0, practiceNoun.TimeFrame);
        
        // Act - Practice incorrectly again
        await service.UpsertNounProgressAsync(TestConstants.DefaultUserId, nounProgress.NounId, false);
        
        // Assert - Should stay at 0 (boundary condition)
        practiceNoun = await service.GetPractiseNounAsync(TestConstants.DefaultUserId, nounProgress.NounId);
        Assert.NotNull(practiceNoun);
        Assert.Equal(0, practiceNoun.TimeFrame);
    }

    [Fact]
    public async Task TestLearning_NounProgressesAdjusted()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();

        // Set up all nouns with progress, then modify one
        var result = await service.UpdateAllNounProgressAsync(TestConstants.DefaultUserId);
        var oneNounProgress = result.First();
        await service.DeleteNounProgressAsync(TestConstants.DefaultUserId, oneNounProgress.NounId);

        // Act - Get practice nouns (should prioritize the one without progress)
        var results = await service.GetNewPractiseNounsAsync(TestConstants.DefaultUserId);
        
        // Assert
        Assert.NotNull(results);
        var languageNouns = results.ToList();
        Assert.Equal(TestConstants.DefaultNounLimit, languageNouns.Count);
        Assert.Equal(oneNounProgress.NounId, languageNouns[0].Id);

        // Act - Practice this noun multiple times
        await service.UpsertNounProgressAsync(TestConstants.DefaultUserId, oneNounProgress.NounId, true); // TimeFrame = 1
        var progress = await service.GetPractiseNounAsync(TestConstants.DefaultUserId, oneNounProgress.NounId);
        Assert.NotNull(progress);
        Assert.Equal(1, progress.TimeFrame);

        await service.UpsertNounProgressAsync(TestConstants.DefaultUserId, oneNounProgress.NounId, true); // TimeFrame = 2
        progress = await service.GetPractiseNounAsync(TestConstants.DefaultUserId, oneNounProgress.NounId);
        Assert.NotNull(progress);
        Assert.Equal(2, progress.TimeFrame);

        await service.UpsertNounProgressAsync(TestConstants.DefaultUserId, oneNounProgress.NounId, true); // TimeFrame = 3
        progress = await service.GetPractiseNounAsync(TestConstants.DefaultUserId, oneNounProgress.NounId);
        Assert.NotNull(progress);
        Assert.Equal(3, progress.TimeFrame);

        // Act - Practice incorrectly (should decrease TimeFrame)
        await service.UpsertNounProgressAsync(TestConstants.DefaultUserId, oneNounProgress.NounId, false);
        progress = await service.GetPractiseNounAsync(TestConstants.DefaultUserId, oneNounProgress.NounId);
        Assert.NotNull(progress);
        Assert.Equal(2, progress.TimeFrame);

        // Assert - Noun should no longer be first in practice list
        results = await service.GetNewPractiseNounsAsync(TestConstants.DefaultUserId);
        Assert.NotNull(results);
        Assert.Equal(TestConstants.DefaultNounLimit, results.Count);
        Assert.NotEqual(oneNounProgress.NounId, results[0].Id);
        Assert.DoesNotContain(oneNounProgress.NounId, results.Select(x => x.Id));
    }

    [Fact]
    public async Task Test_UserCanBeFetched_ViaUrlAndService()
    {
        // This test ensures consistency between HTTP endpoint and service layer
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();

        // Act - Fetch via HTTP endpoint
        var response = await _client.GetAsync(string.Format(TestConstants.ApiEndpoints.UserById, TestConstants.DefaultUserId));
        response.EnsureSuccessStatusCode();
        var httpUser = await TestHelpers.DeserializeResponseAsync<UserProfile>(response);

        // Act - Fetch via service
        var serviceUser = await service.GetUserAsync(TestConstants.DefaultUserId);

        // Assert - Both should return the same data
        TestHelpers.AssertUserProfile(httpUser, TestConstants.DefaultUserId);
        TestHelpers.AssertUserProfile(serviceUser, TestConstants.DefaultUserId);
        Assert.Equal(httpUser?.Username, serviceUser?.Username);
        Assert.Equal(httpUser?.LanguageLevel, serviceUser?.LanguageLevel);
    }

    [Fact]
    public async Task Test_UserLanguageLevel_CanBeUpdated()
    {
        // Arrange
        var updateRequest = TestHelpers.CreateValidUpdateUserRequest("spiffy", "C1");
        var content = TestHelpers.CreateJsonContent(updateRequest);

        // Act
        var response = await _client.PatchAsync(string.Format(TestConstants.ApiEndpoints.UserById, TestConstants.DefaultUserId), content);

        // Assert
        response.EnsureSuccessStatusCode();
        var updatedUser = await TestHelpers.DeserializeResponseAsync<UserProfile>(response);
        TestHelpers.AssertUserProfile(updatedUser, TestConstants.DefaultUserId, "spiffy", "C1");
    }

    [Fact]
    public async Task Test_UserLanguageLevel_PatchValidation()
    {
        // Arrange
        var invalidRequest = TestHelpers.CreateInvalidUpdateUserRequest();
        var content = TestHelpers.CreateJsonContent(invalidRequest);

        // Act
        var response = await _client.PatchAsync(string.Format(TestConstants.ApiEndpoints.UserById, TestConstants.DefaultUserId), content);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task TestLearning_ProgressThroughLevel()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();
        
        // Clean slate for consistent testing
        await service.DeleteAllNounProgressAsync(TestConstants.DefaultUserId);

        // Get user profile
        var userResponse = await _client.GetAsync(string.Format(TestConstants.ApiEndpoints.UserById, TestConstants.DefaultUserId));
        userResponse.EnsureSuccessStatusCode();
        var user = await TestHelpers.DeserializeResponseAsync<UserProfile>(userResponse);

        // Get initial progress
        var progressResponse = await _client.GetAsync(string.Format(TestConstants.ApiEndpoints.LearningProgress, TestConstants.DefaultUserId));
        progressResponse.EnsureSuccessStatusCode();
        var initialProgress = await TestHelpers.DeserializeResponseAsync<LearningProgressResponse>(progressResponse);

        // Assert initial state
        TestHelpers.AssertLearningProgress(initialProgress, user?.Username ?? "", user?.LanguageLevel ?? "");

        // Act - Practice 5 nouns correctly
        var nouns = await service.GetNewPractiseNounsAsync(TestConstants.DefaultUserId, TestConstants.SmallNounLimit);
        foreach (var noun in nouns)
        {
            await service.UpsertNounProgressAsync(TestConstants.DefaultUserId, noun.Id, true);
        }

        // Assert - Progress should show 5 nouns at TimeFrame 1
        var updatedProgress = await service.GetLearningProgress(TestConstants.DefaultUserId);
        Assert.NotNull(updatedProgress);
        Assert.Equal(initialProgress?.TotalNouns, updatedProgress.TotalNouns);
        Assert.Equal(TestConstants.SmallNounLimit, updatedProgress.NounProgresses[1]);

        // Act - Practice same nouns again (should move to TimeFrame 2)
        foreach (var noun in nouns)
        {
            await service.UpsertNounProgressAsync(TestConstants.DefaultUserId, noun.Id, true);
        }

        // Assert - Progress should show gap at TimeFrame 1, and 5 nouns at TimeFrame 2
        updatedProgress = await service.GetLearningProgress(TestConstants.DefaultUserId);
        Assert.NotNull(updatedProgress);
        Assert.Equal(0, updatedProgress.NounProgresses[1]);
        Assert.Equal(TestConstants.SmallNounLimit, updatedProgress.NounProgresses[2]);

        // Act - Practice one noun incorrectly (should move back to TimeFrame 1)
        await service.UpsertNounProgressAsync(TestConstants.DefaultUserId, nouns.First().Id, false);

        // Assert - Final state
        updatedProgress = await service.GetLearningProgress(TestConstants.DefaultUserId);
        Assert.NotNull(updatedProgress);
        Assert.Equal(1, updatedProgress.NounProgresses[1]);
        Assert.Equal(TestConstants.SmallNounLimit - 1, updatedProgress.NounProgresses[2]);
    }
}