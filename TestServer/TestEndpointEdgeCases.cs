using System.Net;
using System.Text;
using System.Text.Json;
using LangVoyageServer.Database;
using LangVoyageServer.Models;
using LangVoyageServer.Requests;

namespace TestServer;

[Collection("Sequential")]
public class TestEndpointEdgeCases : IClassFixture<TestWebApplicationFactory<Program>>
{
    private readonly TestWebApplicationFactory<Program> _factory;
    private static bool _databaseInitialized;
    private static readonly object _lock = new object();

    public TestEndpointEdgeCases(TestWebApplicationFactory<Program> factory)
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
    /// Verifies that GET /user/v1/{id} returns 404 Not Found when requesting a non-existent user.
    /// This test ensures proper error handling for invalid user ID requests in the user API endpoint.
    /// </summary>
    [Fact]
    public async Task GetUser_WithNonExistentId_ReturnsNotFound()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/user/v1/99999");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    /// <summary>
    /// Verifies that PATCH /user/v1/{id} returns 400 Bad Request when sent with empty JSON body.
    /// This test ensures proper input validation and error handling for malformed update requests.
    /// </summary>
    [Fact]
    public async Task UpdateUser_WithEmptyRequestBody_ReturnsBadRequest()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.PatchAsync("/user/v1/1", 
            new StringContent("", Encoding.UTF8, "application/json"));

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    /// <summary>
    /// Verifies that PATCH /user/v1/{id} returns 400 Bad Request when sent with malformed JSON.
    /// This test ensures robust input validation and prevents processing of corrupted request data.
    /// </summary>
    [Fact]
    public async Task UpdateUser_WithInvalidJson_ReturnsBadRequest()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.PatchAsync("/user/v1/1", 
            new StringContent("invalid json", Encoding.UTF8, "application/json"));

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    /// <summary>
    /// Verifies that PATCH /user/v1/{id} returns 404 Not Found when attempting to update a non-existent user.
    /// This test ensures proper error handling for update requests targeting invalid user IDs.
    /// </summary>
    [Fact]
    public async Task UpdateUser_WithNonExistentUser_ReturnsNotFound()
    {
        // Arrange
        var client = _factory.CreateClient();
        var request = new UpdateUserRequest
        {
            Username = "test_user",
            LanguageLevel = "A1"
        };

        // Act
        var response = await client.PatchAsync("/user/v1/99999", 
            new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json"));

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    /// <summary>
    /// Verifies that GET /learn/v1/{userId}/noun returns 500 Internal Server Error for non-existent users.
    /// This test ensures proper error handling when practice noun requests are made for invalid user IDs.
    /// </summary>
    [Fact]
    public async Task GetPractiseNouns_WithNonExistentUser_ReturnsInternalServerError()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/learn/v1/99999/noun");

        // Assert
        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }

    /// <summary>
    /// Verifies that PUT /learn/v1/{userId}/noun returns 400 Bad Request when sent with malformed JSON.
    /// This test ensures robust input validation for noun progress update requests.
    /// </summary>
    [Fact]
    public async Task UpdateNounProgress_WithInvalidRequest_ReturnsBadRequest()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.PutAsync("/learn/v1/1/noun",
            new StringContent("invalid json", Encoding.UTF8, "application/json"));

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    /// <summary>
    /// Verifies that PUT /learn/v1/{userId}/noun returns 500 Internal Server Error for non-existent users.
    /// This test ensures proper error handling when noun progress updates are attempted for invalid user IDs.
    /// </summary>
    [Fact]
    public async Task UpdateNounProgress_WithNonExistentUser_ReturnsInternalServerError()
    {
        // Arrange
        var client = _factory.CreateClient();
        var request = new NounProgressRequest
        {
            NounId = 1,
            AnswerWasCorrect = true
        };

        // Act
        var response = await client.PutAsync("/learn/v1/99999/noun",
            new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json"));

        // Assert
        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }

    /// <summary>
    /// Verifies that GET /learn/v1/{userId}/progress returns 500 Internal Server Error for non-existent users.
    /// This test ensures proper error handling when learning progress is requested for invalid user IDs.
    /// </summary>
    [Fact]
    public async Task GetLearningProgress_WithNonExistentUser_ReturnsInternalServerError()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/learn/v1/99999/progress");

        // Assert
        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }

    /// <summary>
    /// Verifies that GET /user/v1/{id} returns 404 Not Found for invalid user ID values including negative numbers and zero.
    /// This parameterized test ensures comprehensive validation of user ID parameters across edge cases.
    /// </summary>
    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task GetUser_WithInvalidId_ReturnsNotFound(int invalidId)
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync($"/user/v1/{invalidId}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    /// <summary>
    /// Verifies that PATCH /user/v1/{id} successfully updates user when only username is provided.
    /// This test ensures partial update functionality works correctly for username-only changes.
    /// </summary>
    [Fact]
    public async Task UpdateUser_WithOnlyUsername_UpdatesSuccessfully()
    {
        // Arrange
        var client = _factory.CreateClient();
        var request = new UpdateUserRequest
        {
            Username = "only_username_test"
        };

        // Act
        var response = await client.PatchAsync("/user/v1/1", 
            new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json"));

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var user = JsonSerializer.Deserialize<UserProfile>(content, 
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        
        Assert.NotNull(user);
        Assert.Equal("only_username_test", user.Username);
    }

    /// <summary>
    /// Verifies that PATCH /user/v1/{id} successfully updates user when only language level is provided.
    /// This test ensures partial update functionality works correctly for language level-only changes.
    /// </summary>
    [Fact]
    public async Task UpdateUser_WithOnlyLanguageLevel_UpdatesSuccessfully()
    {
        // Arrange
        var client = _factory.CreateClient();
        var request = new UpdateUserRequest
        {
            LanguageLevel = "B2"
        };

        // Act
        var response = await client.PatchAsync("/user/v1/1", 
            new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json"));

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var user = JsonSerializer.Deserialize<UserProfile>(content, 
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        
        Assert.NotNull(user);
        Assert.Equal("B2", user.LanguageLevel);
    }

    /// <summary>
    /// Verifies that DELETE /learn/v1/{userId}/noun returns 204 No Content when deleting all noun progress.
    /// This test ensures proper response status for bulk progress deletion operations.
    /// </summary>
    [Fact]
    public async Task DeleteAllNounProgress_ReturnsNoContent()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.DeleteAsync("/learn/v1/1/noun");

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    /// <summary>
    /// Verifies that GET /learn/v1/{userId}/noun returns valid noun data with proper structure and content.
    /// This test ensures the practice noun endpoint provides correctly formatted learning content.
    /// </summary>
    [Fact]
    public async Task GetPractiseNouns_ReturnsValidNouns()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/learn/v1/1/noun");

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var nouns = JsonSerializer.Deserialize<LanguageNoun[]>(content, 
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        
        Assert.NotNull(nouns);
        Assert.True(nouns.Length > 0);
        
        foreach (var noun in nouns)
        {
            Assert.NotNull(noun.Noun);
            Assert.NotNull(noun.Article);
            Assert.NotNull(noun.Level);
            Assert.True(noun.Id > 0);
        }
    }

    /// <summary>
    /// Verifies that PUT /learn/v1/{userId}/noun successfully processes valid noun progress update requests.
    /// This test ensures the progress update endpoint correctly handles valid practice session data.
    /// </summary>
    [Fact]
    public async Task UpdateNounProgress_WithValidRequest_ReturnsSuccess()
    {
        // Arrange
        var client = _factory.CreateClient();
        
        // First get a noun to practice
        var nounsResponse = await client.GetAsync("/learn/v1/1/noun");
        nounsResponse.EnsureSuccessStatusCode();
        var nounsContent = await nounsResponse.Content.ReadAsStringAsync();
        var nouns = JsonSerializer.Deserialize<LanguageNoun[]>(nounsContent, 
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        
        var firstNoun = nouns!.First();
        var request = new NounProgressRequest
        {
            NounId = firstNoun.Id,
            AnswerWasCorrect = true
        };

        // Act
        var response = await client.PutAsync("/learn/v1/1/noun",
            new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json"));

        // Assert
        response.EnsureSuccessStatusCode();
    }
}