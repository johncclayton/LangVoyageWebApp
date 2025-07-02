using System.Net;
using LangVoyageServer.Database;
using LangVoyageServer.Models;
using LangVoyageServer.Requests;

namespace TestServer;

/// <summary>
/// Tests for user profile management endpoints and functionality
/// </summary>
[Collection("Sequential")]
public class UserProfileTests : IClassFixture<TestWebApplicationFactory<Program>>
{
    private readonly TestWebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public UserProfileTests(TestWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = TestHelpers.CreateTestClient(factory);
        
        // Initialize database once per test class
        DatabaseTestHelper.InitializeDatabase(factory);
    }

    [Fact]
    public async Task GetUser_WithValidId_ReturnsUser()
    {
        // Act
        var response = await _client.GetAsync(string.Format(TestConstants.ApiEndpoints.UserById, TestConstants.DefaultUserId));
        
        // Assert
        response.EnsureSuccessStatusCode();
        var user = await TestHelpers.DeserializeResponseAsync<UserProfile>(response);
        TestHelpers.AssertUserProfile(user, TestConstants.DefaultUserId);
    }

    [Fact]
    public async Task GetUser_WithInvalidId_ReturnsNotFound()
    {
        // Act
        var response = await _client.GetAsync(string.Format(TestConstants.ApiEndpoints.UserById, TestConstants.NonExistentUserId));
        
        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Theory]
    [InlineData("UpdatedUser", "C1")]
    [InlineData("AnotherUser", "A1")]
    [InlineData(null, "B1")] // Test partial update - only language level
    [InlineData("OnlyUsername", null)] // Test partial update - only username
    public async Task UpdateUser_WithValidData_UpdatesSuccessfully(string? username, string? languageLevel)
    {
        // Arrange
        var updateRequest = new UpdateUserRequest
        {
            Username = username,
            LanguageLevel = languageLevel
        };
        var content = TestHelpers.CreateJsonContent(updateRequest);

        // Act
        var response = await _client.PatchAsync(string.Format(TestConstants.ApiEndpoints.UserById, TestConstants.DefaultUserId), content);

        // Assert
        response.EnsureSuccessStatusCode();
        var updatedUser = await TestHelpers.DeserializeResponseAsync<UserProfile>(response);
        TestHelpers.AssertUserProfile(updatedUser, TestConstants.DefaultUserId, username, languageLevel);
    }

    [Fact]
    public async Task UpdateUser_WithInvalidLanguageLevel_ReturnsBadRequest()
    {
        // Arrange
        var updateRequest = TestHelpers.CreateInvalidUpdateUserRequest();
        var content = TestHelpers.CreateJsonContent(updateRequest);

        // Act
        var response = await _client.PatchAsync(string.Format(TestConstants.ApiEndpoints.UserById, TestConstants.DefaultUserId), content);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UpdateUser_WithNonExistentUserId_ReturnsNotFound()
    {
        // Arrange
        var updateRequest = TestHelpers.CreateValidUpdateUserRequest();
        var content = TestHelpers.CreateJsonContent(updateRequest);

        // Act
        var response = await _client.PatchAsync(string.Format(TestConstants.ApiEndpoints.UserById, TestConstants.NonExistentUserId), content);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public async Task UpdateUser_WithEmptyUsername_HandlesGracefully(string? username)
    {
        // Arrange
        var updateRequest = new UpdateUserRequest
        {
            Username = username,
            LanguageLevel = "B1"
        };
        var content = TestHelpers.CreateJsonContent(updateRequest);

        // Act
        var response = await _client.PatchAsync(string.Format(TestConstants.ApiEndpoints.UserById, TestConstants.DefaultUserId), content);

        // Assert
        // Should either succeed (allowing empty username) or return bad request
        Assert.True(response.IsSuccessStatusCode || response.StatusCode == HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task UpdateUser_WithMalformedJson_ReturnsBadRequest()
    {
        // Arrange
        var malformedJson = "{ invalid json }";
        var content = new StringContent(malformedJson, System.Text.Encoding.UTF8, TestConstants.ContentTypes.ApplicationJson);

        // Act
        var response = await _client.PatchAsync(string.Format(TestConstants.ApiEndpoints.UserById, TestConstants.DefaultUserId), content);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task UserProfile_ServiceAndEndpoint_ReturnConsistentData()
    {
        // This test ensures the HTTP endpoint and service layer return the same data
        using var scope = _factory.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IStorageService>();

        // Act - Get via HTTP endpoint
        var httpResponse = await _client.GetAsync(string.Format(TestConstants.ApiEndpoints.UserById, TestConstants.DefaultUserId));
        httpResponse.EnsureSuccessStatusCode();
        var httpUser = await TestHelpers.DeserializeResponseAsync<UserProfile>(httpResponse);

        // Act - Get via service
        var serviceUser = await service.GetUserAsync(TestConstants.DefaultUserId);

        // Assert
        Assert.NotNull(httpUser);
        Assert.NotNull(serviceUser);
        Assert.Equal(httpUser.Id, serviceUser.Id);
        Assert.Equal(httpUser.Username, serviceUser.Username);
        Assert.Equal(httpUser.LanguageLevel, serviceUser.LanguageLevel);
    }
}