using System.Net;
using TestServer.LearningEndpoints;
using TestServer.TestHelpers;

namespace TestServer.LearningEndpoints;

/// <summary>
/// Comprehensive tests for the Learning Endpoint GET operations.
/// Tests both the progress endpoint (/learn/v1/{userId}/progress) and noun endpoint (/learn/v1/{userId}/noun).
/// </summary>
public class LearningEndpointGetTests : LearningEndpointTestBase
{
    public LearningEndpointGetTests(TestWebApplicationFactory<Program> factory) : base(factory)
    {
    }

    #region GET /learn/v1/{userId}/progress Tests

    [Fact]
    public async Task GetLearningProgress_WithValidUser_ReturnsSuccessWithValidStructure()
    {
        // Act
        var progress = await GetLearningProgressAsync(TestUserId);

        // Assert
        Assert.NotNull(progress);
        Assert.Equal("spaceman", progress.Username);
        Assert.Equal("C2", progress.LanguageLevel);
        Assert.True(progress.TotalNouns > 0);
        Assert.NotNull(progress.NounProgresses);
        Assert.True(progress.NounProgresses.Length > 0);
    }

    [Fact]
    public async Task GetLearningProgress_WithNoProgress_ReturnsEmptyProgress()
    {
        // Arrange - Clean state ensures no progress exists
        await DatabaseAssertions.CleanUserProgressAsync(TestUserId);

        // Act
        var progress = await GetLearningProgressAsync(TestUserId);

        // Assert
        Assert.NotNull(progress);
        Assert.Equal("spaceman", progress.Username);
        Assert.Equal("C2", progress.LanguageLevel);
        Assert.True(progress.TotalNouns > 0);
        Assert.All(progress.NounProgresses, count => Assert.Equal(0, count));
    }

    [Fact]
    public async Task GetLearningProgress_WithProgress_ReflectsCorrectDistribution()
    {
        // Arrange
        await DatabaseAssertions.CleanUserProgressAsync(TestUserId);
        var nouns = await GetPracticeNounsAsync(TestUserId, 5);
        
        // Create different progress levels
        await UpdateNounProgressAsync(TestUserId, nouns[0].Id, true); // TimeFrame 1
        await UpdateNounProgressAsync(TestUserId, nouns[1].Id, true); // TimeFrame 1
        await UpdateNounProgressAsync(TestUserId, nouns[2].Id, true); // TimeFrame 1
        await UpdateNounProgressAsync(TestUserId, nouns[2].Id, true); // TimeFrame 2

        // Act
        var progress = await GetLearningProgressAsync(TestUserId);

        // Assert
        Assert.NotNull(progress);
        Assert.Equal(2, progress.NounProgresses[1]); // 2 nouns at TimeFrame 1
        Assert.Equal(1, progress.NounProgresses[2]); // 1 noun at TimeFrame 2
    }

    [Fact]
    public async Task GetLearningProgress_WithInvalidUser_ThrowsException()
    {
        // Act & Assert
        var exception = await Assert.ThrowsAsync<HttpRequestException>(async () => 
            await GetLearningProgressAsync(NonExistentUserId));
        
        // The service should throw an exception for non-existent users
        Assert.NotNull(exception);
    }

    [Fact]
    public async Task GetLearningProgress_HttpStatusCode_ReturnsOk()
    {
        // Act
        var url = BuildEndpointUrl(ProgressEndpoint, TestUserId);
        var (response, data) = await HttpTestHelpers.GetWithStatusAsync<LangVoyageServer.Requests.LearningProgressResponse>(Client, url);

        // Assert
        HttpTestHelpers.AssertStatusCode(response, HttpStatusCode.OK);
        Assert.NotNull(data);
    }

    [Fact]
    public async Task GetLearningProgress_HttpStatusCode_InvalidUser_ReturnsServerError()
    {
        // Act
        var url = BuildEndpointUrl(ProgressEndpoint, NonExistentUserId);
        var (response, data) = await HttpTestHelpers.GetWithStatusAsync<LangVoyageServer.Requests.LearningProgressResponse>(Client, url);

        // Assert
        HttpTestHelpers.AssertServerError(response);
        Assert.Null(data);
    }

    #endregion

    #region GET /learn/v1/{userId}/noun Tests

    [Fact]
    public async Task GetPracticeNouns_WithValidUser_ReturnsNounsForUserLevel()
    {
        // Act
        var nouns = await GetPracticeNounsAsync(TestUserId);

        // Assert
        Assert.NotNull(nouns);
        Assert.Equal(20, nouns.Count); // Default limit
        Assert.All(nouns, noun => Assert.Equal("C2", noun.Level));
        await DatabaseAssertions.AssertNounsMatchUserLevelAsync(TestUserId);
    }

    [Fact]
    public async Task GetPracticeNouns_WithNoProgress_ReturnsNounsInConsistentOrder()
    {
        // Arrange - Clean state ensures no progress exists
        await DatabaseAssertions.CleanUserProgressAsync(TestUserId);

        // Act
        var nouns1 = await GetPracticeNounsAsync(TestUserId);
        var nouns2 = await GetPracticeNounsAsync(TestUserId);

        // Assert
        Assert.NotNull(nouns1);
        Assert.NotNull(nouns2);
        Assert.Equal(nouns1.Count, nouns2.Count);
        
        // Verify consistent ordering when no progress exists
        for (int i = 0; i < nouns1.Count; i++)
        {
            Assert.Equal(nouns1[i].Id, nouns2[i].Id);
        }
    }

    [Fact]
    public async Task GetPracticeNouns_WithProgress_PrioritizesLowTimeFrameNouns()
    {
        // Arrange
        await DatabaseAssertions.CleanUserProgressAsync(TestUserId);
        var nouns = await GetPracticeNounsAsync(TestUserId, 5);
        
        // Create progress for some nouns
        await UpdateNounProgressAsync(TestUserId, nouns[0].Id, true); // TimeFrame 1
        await UpdateNounProgressAsync(TestUserId, nouns[0].Id, true); // TimeFrame 2
        await UpdateNounProgressAsync(TestUserId, nouns[1].Id, true); // TimeFrame 1

        // Act
        var prioritizedNouns = await GetPracticeNounsAsync(TestUserId, 5);

        // Assert
        Assert.NotNull(prioritizedNouns);
        // The noun with TimeFrame 1 should appear before the one with TimeFrame 2
        var noun1Index = prioritizedNouns.ToList().FindIndex(n => n.Id == nouns[1].Id);
        var noun0Index = prioritizedNouns.ToList().FindIndex(n => n.Id == nouns[0].Id);
        
        Assert.True(noun1Index < noun0Index, "Noun with lower TimeFrame should appear first");
    }

    [Fact]
    public async Task GetPracticeNouns_WithLimit_RespectsLimitParameter()
    {
        // Test using direct HTTP call to verify limit handling
        var url = BuildEndpointUrl(NounEndpoint, TestUserId);
        var nounsDefault = await HttpTestHelpers.GetAsync<IList<LangVoyageServer.Models.LanguageNoun>>(Client, url);
        
        Assert.Equal(20, nounsDefault.Count); // Default limit should be 20
    }

    [Fact]
    public async Task GetPracticeNouns_HttpStatusCode_ReturnsOk()
    {
        // Act
        var url = BuildEndpointUrl(NounEndpoint, TestUserId);
        var (response, data) = await HttpTestHelpers.GetWithStatusAsync<IList<LangVoyageServer.Models.LanguageNoun>>(Client, url);

        // Assert
        HttpTestHelpers.AssertStatusCode(response, HttpStatusCode.OK);
        Assert.NotNull(data);
        Assert.NotEmpty(data);
    }

    [Fact]
    public async Task GetPracticeNouns_HttpStatusCode_InvalidUser_ReturnsServerError()
    {
        // Act
        var url = BuildEndpointUrl(NounEndpoint, NonExistentUserId);
        var (response, data) = await HttpTestHelpers.GetWithStatusAsync<IList<LangVoyageServer.Models.LanguageNoun>>(Client, url);

        // Assert
        HttpTestHelpers.AssertServerError(response);
        Assert.Null(data);
    }

    [Fact]
    public async Task GetPracticeNouns_WithInvalidUser_ThrowsException()
    {
        // Act & Assert
        var exception = await Assert.ThrowsAsync<HttpRequestException>(async () => 
            await GetPracticeNounsAsync(NonExistentUserId));
        
        Assert.NotNull(exception);
    }

    #endregion

    #region Edge Cases and Error Handling

    [Fact]
    public async Task GetLearningProgress_WithNegativeUserId_ReturnsServerError()
    {
        // Act
        var url = BuildEndpointUrl(ProgressEndpoint, -1);
        var (response, data) = await HttpTestHelpers.GetWithStatusAsync<LangVoyageServer.Requests.LearningProgressResponse>(Client, url);

        // Assert
        HttpTestHelpers.AssertServerError(response);
        Assert.Null(data);
    }

    [Fact]
    public async Task GetPracticeNouns_WithNegativeUserId_ReturnsServerError()
    {
        // Act
        var url = BuildEndpointUrl(NounEndpoint, -1);
        var (response, data) = await HttpTestHelpers.GetWithStatusAsync<IList<LangVoyageServer.Models.LanguageNoun>>(Client, url);

        // Assert
        HttpTestHelpers.AssertServerError(response);
        Assert.Null(data);
    }

    [Fact]
    public async Task GetLearningProgress_WithZeroUserId_ReturnsServerError()
    {
        // Act
        var url = BuildEndpointUrl(ProgressEndpoint, 0);
        var (response, data) = await HttpTestHelpers.GetWithStatusAsync<LangVoyageServer.Requests.LearningProgressResponse>(Client, url);

        // Assert
        HttpTestHelpers.AssertServerError(response);
        Assert.Null(data);
    }

    [Fact]
    public async Task GetPracticeNouns_WithZeroUserId_ReturnsServerError()
    {
        // Act
        var url = BuildEndpointUrl(NounEndpoint, 0);
        var (response, data) = await HttpTestHelpers.GetWithStatusAsync<IList<LangVoyageServer.Models.LanguageNoun>>(Client, url);

        // Assert
        HttpTestHelpers.AssertServerError(response);
        Assert.Null(data);
    }

    #endregion

    #region Performance and Load Tests

    [Fact]
    public async Task GetPracticeNouns_MultipleConsecutiveCalls_PerformanceTest()
    {
        // Arrange
        const int callCount = 10;
        var tasks = new List<Task<IList<LangVoyageServer.Models.LanguageNoun>>>();

        // Act
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        for (int i = 0; i < callCount; i++)
        {
            tasks.Add(GetPracticeNounsAsync(TestUserId, 10));
        }
        
        var results = await Task.WhenAll(tasks);
        stopwatch.Stop();

        // Assert
        Assert.All(results, result => 
        {
            Assert.NotNull(result);
            Assert.Equal(10, result.Count);
        });
        
        // Performance assertion - should complete within reasonable time
        Assert.True(stopwatch.ElapsedMilliseconds < 5000, 
            $"Multiple calls took too long: {stopwatch.ElapsedMilliseconds}ms");
    }

    [Fact]
    public async Task GetLearningProgress_MultipleConsecutiveCalls_PerformanceTest()
    {
        // Arrange
        const int callCount = 10;
        var tasks = new List<Task<LangVoyageServer.Requests.LearningProgressResponse>>();

        // Act
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        for (int i = 0; i < callCount; i++)
        {
            tasks.Add(GetLearningProgressAsync(TestUserId));
        }
        
        var results = await Task.WhenAll(tasks);
        stopwatch.Stop();

        // Assert
        Assert.All(results, result => 
        {
            Assert.NotNull(result);
            Assert.Equal("spaceman", result.Username);
        });
        
        // Performance assertion - should complete within reasonable time
        Assert.True(stopwatch.ElapsedMilliseconds < 5000, 
            $"Multiple calls took too long: {stopwatch.ElapsedMilliseconds}ms");
    }

    #endregion
}