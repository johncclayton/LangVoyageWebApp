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