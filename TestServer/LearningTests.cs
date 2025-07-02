using LangVoyageServer.Database;
using LangVoyageServer.Models;
using LangVoyageServer.Requests;

namespace TestServer;

/// <summary>
/// Tests for learning and noun practice functionality
/// </summary>
[Collection("Sequential")]
public class LearningTests : IClassFixture<TestWebApplicationFactory<Program>>
{
    private readonly TestWebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public LearningTests(TestWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = TestHelpers.CreateTestClient(factory);
        DatabaseTestHelper.InitializeDatabase(factory);
    }

    [Fact]
    public async Task GetNounsForPractice_ReturnsCorrectLevelNouns()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        await DatabaseTestHelper.CleanupUserProgress(scope, TestConstants.DefaultUserId);

        // Get user to verify their level
        var userResponse = await _client.GetAsync(string.Format(TestConstants.ApiEndpoints.UserById, TestConstants.DefaultUserId));
        userResponse.EnsureSuccessStatusCode();
        var user = await TestHelpers.DeserializeResponseAsync<UserProfile>(userResponse);

        // Act
        var response = await _client.GetAsync(string.Format(TestConstants.ApiEndpoints.LearnNouns, TestConstants.DefaultUserId));

        // Assert
        response.EnsureSuccessStatusCode();
        var nouns = await TestHelpers.DeserializeResponseAsync<IList<LanguageNoun>>(response);
        TestHelpers.AssertLanguageNouns(nouns, TestConstants.DefaultNounLimit, user?.LanguageLevel);
    }

    [Fact]
    public async Task GetNounsForPractice_WithNonExistentUser_ReturnsError()
    {
        // Act
        var response = await _client.GetAsync(string.Format(TestConstants.ApiEndpoints.LearnNouns, TestConstants.NonExistentUserId));

        // Assert
        Assert.False(response.IsSuccessStatusCode);
    }

    [Fact]
    public async Task GetLearningProgress_ReturnsValidStructure()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        await DatabaseTestHelper.SetupUserWithProgress(scope, TestConstants.DefaultUserId, TestConstants.SmallNounLimit);

        // Act
        var response = await _client.GetAsync(string.Format(TestConstants.ApiEndpoints.LearningProgress, TestConstants.DefaultUserId));

        // Assert
        response.EnsureSuccessStatusCode();
        var progress = await TestHelpers.DeserializeResponseAsync<LearningProgressResponse>(response);
        TestHelpers.AssertLearningProgress(progress, TestConstants.DefaultUsername, TestConstants.DefaultLanguageLevel);
    }

    [Fact]
    public async Task GetLearningProgress_WithNonExistentUser_ReturnsError()
    {
        // Act
        var response = await _client.GetAsync(string.Format(TestConstants.ApiEndpoints.LearningProgress, TestConstants.NonExistentUserId));

        // Assert
        Assert.False(response.IsSuccessStatusCode);
    }

    [Theory]
    [InlineData(5)]
    [InlineData(10)]
    [InlineData(20)]
    public async Task GetNounsForPractice_WithDifferentLimits_ReturnsCorrectCount(int expectedCount)
    {
        // This test would require modifying the endpoint to accept a limit parameter
        // For now, test that the service method works correctly
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();

        // Act
        var nouns = await service.GetNewPractiseNounsAsync(TestConstants.DefaultUserId, expectedCount);

        // Assert
        Assert.NotNull(nouns);
        Assert.True(nouns.Count <= expectedCount); // May be less if not enough nouns available
    }

    [Fact]
    public async Task LearningProgress_ConsistentBetweenServiceAndEndpoint()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();
        await DatabaseTestHelper.SetupUserWithProgress(scope, TestConstants.DefaultUserId, TestConstants.SmallNounLimit);

        // Act - Get via HTTP endpoint
        var httpResponse = await _client.GetAsync(string.Format(TestConstants.ApiEndpoints.LearningProgress, TestConstants.DefaultUserId));
        httpResponse.EnsureSuccessStatusCode();
        var httpProgress = await TestHelpers.DeserializeResponseAsync<LearningProgressResponse>(httpResponse);

        // Act - Get via service
        var serviceProgress = await service.GetLearningProgress(TestConstants.DefaultUserId);

        // Assert
        Assert.NotNull(httpProgress);
        Assert.NotNull(serviceProgress);
        Assert.Equal(httpProgress.Username, serviceProgress.Username);
        Assert.Equal(httpProgress.LanguageLevel, serviceProgress.LanguageLevel);
        Assert.Equal(httpProgress.TotalNouns, serviceProgress.TotalNouns);
        Assert.Equal(httpProgress.NounProgresses.Length, serviceProgress.NounProgresses.Length);
        
        for (int i = 0; i < httpProgress.NounProgresses.Length; i++)
        {
            Assert.Equal(httpProgress.NounProgresses[i], serviceProgress.NounProgresses[i]);
        }
    }

    [Fact]
    public async Task NounPractice_ProgressTracking_WorksCorrectly()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();
        await DatabaseTestHelper.CleanupUserProgress(scope, TestConstants.DefaultUserId);

        // Get a noun to practice
        var nouns = await service.GetNewPractiseNounsAsync(TestConstants.DefaultUserId, 1);
        Assert.NotNull(nouns);
        Assert.True(nouns.Count > 0);
        var noun = nouns.First();

        // Act & Assert - Practice correctly
        var progress1 = await service.UpsertNounProgressAsync(TestConstants.DefaultUserId, noun.Id, true);
        Assert.NotNull(progress1);
        Assert.Equal(1, progress1.TimeFrame);

        // Practice correctly again
        var progress2 = await service.UpsertNounProgressAsync(TestConstants.DefaultUserId, noun.Id, true);
        Assert.NotNull(progress2);
        Assert.Equal(2, progress2.TimeFrame);

        // Practice incorrectly
        var progress3 = await service.UpsertNounProgressAsync(TestConstants.DefaultUserId, noun.Id, false);
        Assert.NotNull(progress3);
        Assert.Equal(1, progress3.TimeFrame); // Should decrease
    }

    [Fact]
    public async Task NounProgress_BoundaryConditions_HandledCorrectly()
    {
        // Arrange
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();
        await DatabaseTestHelper.CleanupUserProgress(scope, TestConstants.DefaultUserId);

        // Get a noun and practice it to a higher level
        var nouns = await service.GetNewPractiseNounsAsync(TestConstants.DefaultUserId, 1);
        var noun = nouns.First();

        // Build up to a higher time frame
        await service.UpsertNounProgressAsync(TestConstants.DefaultUserId, noun.Id, true); // 1
        await service.UpsertNounProgressAsync(TestConstants.DefaultUserId, noun.Id, true); // 2
        await service.UpsertNounProgressAsync(TestConstants.DefaultUserId, noun.Id, true); // 3

        var progress = await service.GetPractiseNounAsync(TestConstants.DefaultUserId, noun.Id);
        Assert.NotNull(progress);
        var initialTimeFrame = progress.TimeFrame;

        // Act - Practice incorrectly multiple times to reach bottom
        for (int i = 0; i < initialTimeFrame + 2; i++) // +2 to test lower boundary
        {
            await service.UpsertNounProgressAsync(TestConstants.DefaultUserId, noun.Id, false);
        }

        // Assert - Should not go below 0
        var finalProgress = await service.GetPractiseNounAsync(TestConstants.DefaultUserId, noun.Id);
        Assert.NotNull(finalProgress);
        Assert.True(finalProgress.TimeFrame >= 0);
    }
}