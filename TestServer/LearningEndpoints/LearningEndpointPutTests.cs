using System.Net;
using System.Text;
using TestServer.LearningEndpoints;
using TestServer.TestHelpers;

namespace TestServer.LearningEndpoints;

/// <summary>
/// Comprehensive tests for the Learning Endpoint PUT operations.
/// Tests the progress update endpoint (/learn/v1/{userId}/noun) with various scenarios.
/// </summary>
public class LearningEndpointPutTests : LearningEndpointTestBase
{
    public LearningEndpointPutTests(TestWebApplicationFactory<Program> factory) : base(factory)
    {
    }

    #region PUT /learn/v1/{userId}/noun Basic Tests

    [Fact]
    public async Task UpdateNounProgress_FirstTimeCorrect_CreatesProgressWithTimeFrame1()
    {
        // Arrange
        await DatabaseAssertions.CleanUserProgressAsync(TestUserId);
        var nouns = await GetPracticeNounsAsync(TestUserId, 1);
        var noun = nouns.First();

        // Act
        var result = await UpdateNounProgressAsync(TestUserId, noun.Id, true);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(TestUserId, result.UserProfileId);
        Assert.Equal(noun.Id, result.NounId);
        Assert.Equal(1, result.TimeFrame);
        
        // Verify in database
        await DatabaseAssertions.AssertNounProgressExistsAsync(TestUserId, noun.Id, 1);
    }

    [Fact]
    public async Task UpdateNounProgress_FirstTimeIncorrect_CreatesProgressWithTimeFrame1()
    {
        // Arrange
        await DatabaseAssertions.CleanUserProgressAsync(TestUserId);
        var nouns = await GetPracticeNounsAsync(TestUserId, 1);
        var noun = nouns.First();

        // Act
        var result = await UpdateNounProgressAsync(TestUserId, noun.Id, false);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(TestUserId, result.UserProfileId);
        Assert.Equal(noun.Id, result.NounId);
        Assert.Equal(1, result.TimeFrame); // First time always starts at 1
        
        // Verify in database
        await DatabaseAssertions.AssertNounProgressExistsAsync(TestUserId, noun.Id, 1);
    }

    [Fact]
    public async Task UpdateNounProgress_CorrectAnswer_IncreasesTimeFrame()
    {
        // Arrange
        await DatabaseAssertions.CleanUserProgressAsync(TestUserId);
        var nouns = await GetPracticeNounsAsync(TestUserId, 1);
        var noun = nouns.First();
        
        // Create initial progress
        await UpdateNounProgressAsync(TestUserId, noun.Id, true); // TimeFrame 1

        // Act
        var result = await UpdateNounProgressAsync(TestUserId, noun.Id, true);

        // Assert
        Assert.Equal(2, result.TimeFrame);
        await DatabaseAssertions.AssertNounProgressExistsAsync(TestUserId, noun.Id, 2);
    }

    [Fact]
    public async Task UpdateNounProgress_IncorrectAnswer_DecreasesTimeFrame()
    {
        // Arrange
        await DatabaseAssertions.CleanUserProgressAsync(TestUserId);
        var nouns = await GetPracticeNounsAsync(TestUserId, 1);
        var noun = nouns.First();
        
        // Create progress at TimeFrame 2
        await UpdateNounProgressAsync(TestUserId, noun.Id, true); // TimeFrame 1
        await UpdateNounProgressAsync(TestUserId, noun.Id, true); // TimeFrame 2

        // Act
        var result = await UpdateNounProgressAsync(TestUserId, noun.Id, false);

        // Assert
        Assert.Equal(1, result.TimeFrame);
        await DatabaseAssertions.AssertNounProgressExistsAsync(TestUserId, noun.Id, 1);
    }

    [Fact]
    public async Task UpdateNounProgress_IncorrectAtTimeFrame0_StaysAt0()
    {
        // Arrange
        await DatabaseAssertions.CleanUserProgressAsync(TestUserId);
        var nouns = await GetPracticeNounsAsync(TestUserId, 1);
        var noun = nouns.First();
        
        // Create progress and reduce to TimeFrame 0
        await UpdateNounProgressAsync(TestUserId, noun.Id, true); // TimeFrame 1
        await UpdateNounProgressAsync(TestUserId, noun.Id, false); // TimeFrame 0

        // Act
        var result = await UpdateNounProgressAsync(TestUserId, noun.Id, false);

        // Assert
        Assert.Equal(0, result.TimeFrame);
        await DatabaseAssertions.AssertNounProgressExistsAsync(TestUserId, noun.Id, 0);
    }

    #endregion

    #region PUT /learn/v1/{userId}/noun Spaced Repetition Logic Tests

    [Fact]
    public async Task UpdateNounProgress_SpacedRepetitionLogic_CorrectProgression()
    {
        // Arrange
        await DatabaseAssertions.CleanUserProgressAsync(TestUserId);
        var nouns = await GetPracticeNounsAsync(TestUserId, 1);
        var noun = nouns.First();

        // Act & Assert - Test progression through multiple correct answers
        var result1 = await UpdateNounProgressAsync(TestUserId, noun.Id, true);
        Assert.Equal(1, result1.TimeFrame);

        var result2 = await UpdateNounProgressAsync(TestUserId, noun.Id, true);
        Assert.Equal(2, result2.TimeFrame);

        var result3 = await UpdateNounProgressAsync(TestUserId, noun.Id, true);
        Assert.Equal(3, result3.TimeFrame);

        var result4 = await UpdateNounProgressAsync(TestUserId, noun.Id, true);
        Assert.Equal(4, result4.TimeFrame);
    }

    [Fact]
    public async Task UpdateNounProgress_SpacedRepetitionLogic_MixedAnswers()
    {
        // Arrange
        await DatabaseAssertions.CleanUserProgressAsync(TestUserId);
        var nouns = await GetPracticeNounsAsync(TestUserId, 1);
        var noun = nouns.First();

        // Act & Assert - Test mixed correct/incorrect pattern
        await UpdateNounProgressAsync(TestUserId, noun.Id, true);  // 1
        await UpdateNounProgressAsync(TestUserId, noun.Id, true);  // 2
        await UpdateNounProgressAsync(TestUserId, noun.Id, true);  // 3
        
        var result1 = await UpdateNounProgressAsync(TestUserId, noun.Id, false); // 2
        Assert.Equal(2, result1.TimeFrame);
        
        var result2 = await UpdateNounProgressAsync(TestUserId, noun.Id, false); // 1
        Assert.Equal(1, result2.TimeFrame);
        
        var result3 = await UpdateNounProgressAsync(TestUserId, noun.Id, false); // 0
        Assert.Equal(0, result3.TimeFrame);
        
        var result4 = await UpdateNounProgressAsync(TestUserId, noun.Id, false); // Still 0
        Assert.Equal(0, result4.TimeFrame);
        
        var result5 = await UpdateNounProgressAsync(TestUserId, noun.Id, true);  // 1
        Assert.Equal(1, result5.TimeFrame);
    }

    [Fact]
    public async Task UpdateNounProgress_LastPractisedTimestamp_UpdatesCorrectly()
    {
        // Arrange
        await DatabaseAssertions.CleanUserProgressAsync(TestUserId);
        var nouns = await GetPracticeNounsAsync(TestUserId, 1);
        var noun = nouns.First();
        var beforeTime = DateTime.UtcNow;

        // Act
        await UpdateNounProgressAsync(TestUserId, noun.Id, true);
        var afterTime = DateTime.UtcNow;

        // Assert
        var progress = await DatabaseAssertions.StorageService.GetPractiseNounAsync(TestUserId, noun.Id);
        Assert.NotNull(progress);
        Assert.True(progress.LastPractised >= beforeTime && progress.LastPractised <= afterTime,
            $"LastPractised {progress.LastPractised} should be between {beforeTime} and {afterTime}");
    }

    #endregion

    #region PUT /learn/v1/{userId}/noun HTTP Status Code Tests

    [Fact]
    public async Task UpdateNounProgress_ValidRequest_ReturnsOk()
    {
        // Arrange
        await DatabaseAssertions.CleanUserProgressAsync(TestUserId);
        var nouns = await GetPracticeNounsAsync(TestUserId, 1);
        var noun = nouns.First();
        var request = TestDataBuilder.NounProgressRequest()
            .WithNounId(noun.Id)
            .WithAnswerWasCorrect(true)
            .Build();

        // Act
        var url = BuildEndpointUrl(UpdateProgressEndpoint, TestUserId);
        var (response, data) = await HttpTestHelpers.PutWithStatusAsync<LangVoyageServer.Models.NounProgress>(Client, url, request);

        // Assert
        HttpTestHelpers.AssertStatusCode(response, HttpStatusCode.OK);
        Assert.NotNull(data);
        Assert.Equal(noun.Id, data.NounId);
    }

    [Fact]
    public async Task UpdateNounProgress_InvalidUser_ReturnsServerError()
    {
        // Arrange
        var request = TestDataBuilder.NounProgressRequest()
            .WithNounId(1)
            .WithAnswerWasCorrect(true)
            .Build();

        // Act
        var url = BuildEndpointUrl(UpdateProgressEndpoint, NonExistentUserId);
        var (response, data) = await HttpTestHelpers.PutWithStatusAsync<LangVoyageServer.Models.NounProgress>(Client, url, request);

        // Assert
        HttpTestHelpers.AssertServerError(response);
        Assert.Null(data);
    }

    [Fact]
    public async Task UpdateNounProgress_InvalidNoun_ReturnsServerError()
    {
        // Arrange
        var request = TestDataBuilder.NounProgressRequest()
            .WithNounId(999999) // Non-existent noun
            .WithAnswerWasCorrect(true)
            .Build();

        // Act
        var url = BuildEndpointUrl(UpdateProgressEndpoint, TestUserId);
        var (response, data) = await HttpTestHelpers.PutWithStatusAsync<LangVoyageServer.Models.NounProgress>(Client, url, request);

        // Assert
        HttpTestHelpers.AssertServerError(response);
        Assert.Null(data);
    }

    [Fact]
    public async Task UpdateNounProgress_MalformedJson_ReturnsBadRequest()
    {
        // Act
        var url = BuildEndpointUrl(UpdateProgressEndpoint, TestUserId);
        var malformedJson = new StringContent("{invalid json", Encoding.UTF8, "application/json");
        var response = await Client.PutAsync(url, malformedJson);

        // Assert
        HttpTestHelpers.AssertClientError(response);
    }

    [Fact]
    public async Task UpdateNounProgress_EmptyBody_ReturnsBadRequest()
    {
        // Act
        var url = BuildEndpointUrl(UpdateProgressEndpoint, TestUserId);
        var emptyContent = new StringContent("", Encoding.UTF8, "application/json");
        var response = await Client.PutAsync(url, emptyContent);

        // Assert
        HttpTestHelpers.AssertClientError(response);
    }

    #endregion

    #region PUT /learn/v1/{userId}/noun Edge Cases

    [Fact]
    public async Task UpdateNounProgress_NegativeNounId_ReturnsServerError()
    {
        // Arrange
        var request = TestDataBuilder.NounProgressRequest()
            .WithNounId(-1)
            .WithAnswerWasCorrect(true)
            .Build();

        // Act
        var url = BuildEndpointUrl(UpdateProgressEndpoint, TestUserId);
        var (response, data) = await HttpTestHelpers.PutWithStatusAsync<LangVoyageServer.Models.NounProgress>(Client, url, request);

        // Assert
        HttpTestHelpers.AssertServerError(response);
        Assert.Null(data);
    }

    [Fact]
    public async Task UpdateNounProgress_ZeroNounId_ReturnsServerError()
    {
        // Arrange
        var request = TestDataBuilder.NounProgressRequest()
            .WithNounId(0)
            .WithAnswerWasCorrect(true)
            .Build();

        // Act
        var url = BuildEndpointUrl(UpdateProgressEndpoint, TestUserId);
        var (response, data) = await HttpTestHelpers.PutWithStatusAsync<LangVoyageServer.Models.NounProgress>(Client, url, request);

        // Assert
        HttpTestHelpers.AssertServerError(response);
        Assert.Null(data);
    }

    [Fact]
    public async Task UpdateNounProgress_MultipleUsersProgress_IndependentlyManaged()
    {
        // Arrange
        await DatabaseAssertions.CleanUserProgressAsync(TestUserId);
        await DatabaseAssertions.CleanUserProgressAsync(SecondTestUserId);
        
        var nouns = await GetPracticeNounsAsync(TestUserId, 2);
        var noun1 = nouns[0];
        var noun2 = nouns[1];

        // Act - Create progress for both users on same noun
        var user1Result1 = await UpdateNounProgressAsync(TestUserId, noun1.Id, true);
        var user2Result1 = await UpdateNounProgressAsync(SecondTestUserId, noun1.Id, true);
        
        var user1Result2 = await UpdateNounProgressAsync(TestUserId, noun1.Id, true);
        var user2Result2 = await UpdateNounProgressAsync(SecondTestUserId, noun1.Id, false);

        // Assert - Progress should be independent
        Assert.Equal(2, user1Result2.TimeFrame);
        Assert.Equal(0, user2Result2.TimeFrame);
        
        await DatabaseAssertions.AssertNounProgressExistsAsync(TestUserId, noun1.Id, 2);
        await DatabaseAssertions.AssertNounProgressExistsAsync(SecondTestUserId, noun1.Id, 0);
    }

    #endregion

    #region PUT /learn/v1/{userId}/noun Performance and Concurrency Tests

    [Fact]
    public async Task UpdateNounProgress_ConcurrentUpdates_HandledCorrectly()
    {
        // Arrange
        await DatabaseAssertions.CleanUserProgressAsync(TestUserId);
        var nouns = await GetPracticeNounsAsync(TestUserId, 1);
        var noun = nouns.First();

        // Act - Create multiple concurrent updates
        var tasks = new List<Task<LangVoyageServer.Models.NounProgress>>();
        for (int i = 0; i < 5; i++)
        {
            tasks.Add(UpdateNounProgressAsync(TestUserId, noun.Id, true));
        }

        var results = await Task.WhenAll(tasks);

        // Assert - Final state should be consistent (TimeFrame should be 5)
        var finalProgress = await DatabaseAssertions.StorageService.GetPractiseNounAsync(TestUserId, noun.Id);
        Assert.NotNull(finalProgress);
        Assert.Equal(5, finalProgress.TimeFrame);
        
        // All results should have valid data
        Assert.All(results, result => 
        {
            Assert.NotNull(result);
            Assert.Equal(noun.Id, result.NounId);
            Assert.Equal(TestUserId, result.UserProfileId);
        });
    }

    [Fact]
    public async Task UpdateNounProgress_BulkUpdates_PerformanceTest()
    {
        // Arrange
        await DatabaseAssertions.CleanUserProgressAsync(TestUserId);
        var nouns = await GetPracticeNounsAsync(TestUserId, 10);

        // Act
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        var tasks = nouns.Select(noun => UpdateNounProgressAsync(TestUserId, noun.Id, true)).ToArray();
        var results = await Task.WhenAll(tasks);
        stopwatch.Stop();

        // Assert
        Assert.Equal(nouns.Count, results.Length);
        Assert.All(results, result => 
        {
            Assert.NotNull(result);
            Assert.Equal(1, result.TimeFrame); // First practice should be TimeFrame 1
        });
        
        // Performance assertion
        Assert.True(stopwatch.ElapsedMilliseconds < 3000, 
            $"Bulk updates took too long: {stopwatch.ElapsedMilliseconds}ms");
    }

    #endregion
}