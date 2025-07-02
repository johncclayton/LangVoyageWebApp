using System.Net;
using System.Text;
using System.Text.Json;
using LangVoyageServer.Database;
using LangVoyageServer.Models;
using LangVoyageServer.Requests;

namespace TestServer;

[Collection("Sequential")]
public class UserProfileEndpointTests : IClassFixture<TestWebApplicationFactory<Program>>
{
    private readonly TestWebApplicationFactory<Program> _factory;
    private static bool _databaseInitialized;
    private static readonly object _lock = new object();

    public UserProfileEndpointTests(TestWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        // Note: Database initialization is handled by the Sequential collection
        // We'll create test users as needed in individual tests
    }

    private async Task<int> CreateTestUserAsync(string username, string languageLevel)
    {
        using var scope = _factory.Services.CreateScope();
        var (context, service) = await Utilities.SeedDatabase(scope, deleteDatabase: false);
        
        // Create a new user and let the database assign the ID
        var newUser = await service.UpsertUserProfileAsync(0, new UpdateUserRequest
        {
            Username = username,
            LanguageLevel = languageLevel
        });

        return newUser!.Id;
    }

    #region GET Endpoint Tests

    [Fact]
    public async Task GetUserProfile_ExistingUser_ReturnsUserProfile()
    {
        // Arrange
        var client = _factory.CreateClient();
        var testUserId = await CreateTestUserAsync("userprofile_test_user", "B1");
        
        // Act
        var response = await client.GetAsync($"/user/v1/{testUserId}");
        
        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var user = JsonSerializer.Deserialize<UserProfile>(content, 
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        
        Assert.NotNull(user);
        Assert.Equal(testUserId, user.Id);
        Assert.Equal("userprofile_test_user", user.Username);
        Assert.Equal("B1", user.LanguageLevel);
    }

    [Fact]
    public async Task GetUserProfile_NonExistentUser_ReturnsNotFound()
    {
        // Arrange
        var client = _factory.CreateClient();
        
        // Act
        var response = await client.GetAsync("/user/v1/999");
        
        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetUserProfile_InvalidIdFormat_ReturnsBadRequest()
    {
        // Arrange
        var client = _factory.CreateClient();
        
        // Act
        var response = await client.GetAsync("/user/v1/invalid");
        
        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode); // Route doesn't match
    }

    #endregion

    #region PATCH Endpoint Tests

    [Fact]
    public async Task UpdateUserProfile_ValidRequest_UpdatesAndReturnsUser()
    {
        // Arrange
        var client = _factory.CreateClient();
        var testUserId = await CreateTestUserAsync("original_user", "B2");
        
        var updateRequest = new UpdateUserRequest
        {
            Username = "updateduser",
            LanguageLevel = "A2"
        };
        var jsonContent = new StringContent(
            JsonSerializer.Serialize(updateRequest),
            Encoding.UTF8,
            "application/json");

        // Act
        var response = await client.PatchAsync($"/user/v1/{testUserId}", jsonContent);

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var user = JsonSerializer.Deserialize<UserProfile>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        Assert.NotNull(user);
        Assert.Equal(testUserId, user.Id);
        Assert.Equal("updateduser", user.Username);
        Assert.Equal("A2", user.LanguageLevel);
    }

    [Fact]
    public async Task UpdateUserProfile_UsernameOnly_UpdatesUsernameKeepsLanguageLevel()
    {
        // Arrange
        var client = _factory.CreateClient();
        var testUserId = await CreateTestUserAsync("original_username", "A1");
        
        var updateRequest = new UpdateUserRequest
        {
            Username = "usernameonly"
            // LanguageLevel intentionally null
        };
        var jsonContent = new StringContent(
            JsonSerializer.Serialize(updateRequest),
            Encoding.UTF8,
            "application/json");

        // Act
        var response = await client.PatchAsync($"/user/v1/{testUserId}", jsonContent);

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var user = JsonSerializer.Deserialize<UserProfile>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        Assert.NotNull(user);
        Assert.Equal(testUserId, user.Id);
        Assert.Equal("usernameonly", user.Username);
        Assert.Equal("A1", user.LanguageLevel); // Should remain unchanged
    }

    [Fact]
    public async Task UpdateUserProfile_LanguageLevelOnly_UpdatesLanguageLevelKeepsUsername()
    {
        // Arrange
        var client = _factory.CreateClient();
        var testUserId = await CreateTestUserAsync("original_username2", "B1");
        
        var updateRequest = new UpdateUserRequest
        {
            LanguageLevel = "C1"
            // Username intentionally null
        };
        var jsonContent = new StringContent(
            JsonSerializer.Serialize(updateRequest),
            Encoding.UTF8,
            "application/json");

        // Act
        var response = await client.PatchAsync($"/user/v1/{testUserId}", jsonContent);

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var user = JsonSerializer.Deserialize<UserProfile>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        Assert.NotNull(user);
        Assert.Equal(testUserId, user.Id);
        Assert.Equal("original_username2", user.Username); // Should remain unchanged
        Assert.Equal("C1", user.LanguageLevel);
    }

    [Fact]
    public async Task UpdateUserProfile_NonExistentUser_ReturnsNotFound()
    {
        // Arrange
        var client = _factory.CreateClient();
        var updateRequest = new UpdateUserRequest
        {
            Username = "newuser",
            LanguageLevel = "B2"
        };
        var jsonContent = new StringContent(
            JsonSerializer.Serialize(updateRequest),
            Encoding.UTF8,
            "application/json");

        // Act
        var response = await client.PatchAsync("/user/v1/999", jsonContent);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task UpdateUserProfile_InvalidLanguageLevel_ReturnsBadRequest()
    {
        // Arrange
        var client = _factory.CreateClient();
        var testUserId = await CreateTestUserAsync("test_invalid_level", "B1");
        
        var updateRequest = new UpdateUserRequest
        {
            Username = "testuser", 
            LanguageLevel = "Z9" // Invalid language level
        };
        var jsonContent = new StringContent(
            JsonSerializer.Serialize(updateRequest),
            Encoding.UTF8,
            "application/json");

        // Act
        var response = await client.PatchAsync($"/user/v1/{testUserId}", jsonContent);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateUserProfile_EmptyRequest_ReturnsNull()
    {
        // Arrange
        var client = _factory.CreateClient();
        var testUserId = await CreateTestUserAsync("test_empty_request", "B2");
        
        var updateRequest = new UpdateUserRequest(); // Both properties null
        var jsonContent = new StringContent(
            JsonSerializer.Serialize(updateRequest),
            Encoding.UTF8,
            "application/json");

        // Act
        var response = await client.PatchAsync($"/user/v1/{testUserId}", jsonContent);

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        
        // When both properties are null, UpsertUserProfileAsync returns null
        // which results in an empty response body
        Assert.Equal("", content);
    }

    [Fact]
    public async Task UpdateUserProfile_ValidLanguageLevels_AllAccepted()
    {
        // Arrange
        var client = _factory.CreateClient();
        var testUserId = await CreateTestUserAsync("test_all_levels", "A1");
        var validLevels = new[] { "A1", "A2", "B1", "B2", "C1", "C2" };

        foreach (var level in validLevels)
        {
            var updateRequest = new UpdateUserRequest
            {
                Username = "testuser",
                LanguageLevel = level
            };
            var jsonContent = new StringContent(
                JsonSerializer.Serialize(updateRequest),
                Encoding.UTF8,
                "application/json");

            // Act
            var response = await client.PatchAsync($"/user/v1/{testUserId}", jsonContent);

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var user = JsonSerializer.Deserialize<UserProfile>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.NotNull(user);
            Assert.Equal(level, user.LanguageLevel);
        }
    }

    [Fact]
    public async Task UpdateUserProfile_MalformedJson_ReturnsBadRequest()
    {
        // Arrange
        var client = _factory.CreateClient();
        var testUserId = await CreateTestUserAsync("test_malformed", "A2");
        
        var malformedJson = new StringContent(
            "{ invalid json }",
            Encoding.UTF8,
            "application/json");

        // Act
        var response = await client.PatchAsync($"/user/v1/{testUserId}", malformedJson);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateUserProfile_CaseInsensitiveLanguageLevel_WorksCorrectly()
    {
        // Arrange
        var client = _factory.CreateClient();
        var testUserId = await CreateTestUserAsync("test_case_insensitive", "B1");
        
        var updateRequest = new UpdateUserRequest
        {
            Username = "testuser",
            LanguageLevel = "c2" // lowercase
        };
        var jsonContent = new StringContent(
            JsonSerializer.Serialize(updateRequest),
            Encoding.UTF8,
            "application/json");

        // Act
        var response = await client.PatchAsync($"/user/v1/{testUserId}", jsonContent);

        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var user = JsonSerializer.Deserialize<UserProfile>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        Assert.NotNull(user);
        Assert.Equal("c2", user.LanguageLevel);
    }

    #endregion
}