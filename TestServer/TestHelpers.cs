using System.Text;
using System.Text.Json;
using LangVoyageServer.Models;
using LangVoyageServer.Requests;

namespace TestServer;

/// <summary>
/// Helper class providing common utilities for test classes
/// </summary>
public static class TestHelpers
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    /// <summary>
    /// Creates an HTTP client from the test factory
    /// </summary>
    public static HttpClient CreateTestClient(TestWebApplicationFactory<Program> factory)
    {
        return factory.CreateClient();
    }

    /// <summary>
    /// Creates a StringContent object for JSON payloads
    /// </summary>
    public static StringContent CreateJsonContent<T>(T obj)
    {
        var json = JsonSerializer.Serialize(obj);
        return new StringContent(json, Encoding.UTF8, TestConstants.ContentTypes.ApplicationJson);
    }

    /// <summary>
    /// Deserializes JSON response content to the specified type
    /// </summary>
    public static async Task<T?> DeserializeResponseAsync<T>(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(content, JsonOptions);
    }

    /// <summary>
    /// Creates a valid UpdateUserRequest for testing
    /// </summary>
    public static UpdateUserRequest CreateValidUpdateUserRequest(
        string? username = TestConstants.DefaultUsername,
        string? languageLevel = TestConstants.DefaultLanguageLevel)
    {
        return new UpdateUserRequest
        {
            Username = username,
            LanguageLevel = languageLevel
        };
    }

    /// <summary>
    /// Creates an invalid UpdateUserRequest for validation testing
    /// </summary>
    public static UpdateUserRequest CreateInvalidUpdateUserRequest()
    {
        return new UpdateUserRequest
        {
            Username = "test",
            LanguageLevel = TestConstants.InvalidLanguageLevel
        };
    }

    /// <summary>
    /// Asserts that a UserProfile has expected values
    /// </summary>
    public static void AssertUserProfile(UserProfile? user, int expectedId, string? expectedUsername = null, string? expectedLanguageLevel = null)
    {
        Assert.NotNull(user);
        Assert.Equal(expectedId, user.Id);
        
        if (expectedUsername != null)
            Assert.Equal(expectedUsername, user.Username);
            
        if (expectedLanguageLevel != null)
            Assert.Equal(expectedLanguageLevel, user.LanguageLevel);
    }

    /// <summary>
    /// Asserts that a collection of language nouns has expected properties
    /// </summary>
    public static void AssertLanguageNouns(IList<LanguageNoun>? nouns, int expectedCount, string? expectedLevel = null)
    {
        Assert.NotNull(nouns);
        Assert.Equal(expectedCount, nouns.Count);
        
        if (expectedLevel != null)
        {
            foreach (var noun in nouns)
            {
                Assert.Equal(expectedLevel, noun.Level);
            }
        }
    }

    /// <summary>
    /// Asserts that a learning progress response has valid structure
    /// </summary>
    public static void AssertLearningProgress(LearningProgressResponse? progress, string expectedUsername, string expectedLanguageLevel)
    {
        Assert.NotNull(progress);
        Assert.Equal(expectedUsername, progress.Username);
        Assert.Equal(expectedLanguageLevel, progress.LanguageLevel);
        Assert.True(progress.TotalNouns > 0);
        Assert.NotNull(progress.NounProgresses);
        Assert.True(progress.NounProgresses.Length > 0);
    }
}