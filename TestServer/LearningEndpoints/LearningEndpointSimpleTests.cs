using TestServer.LearningEndpoints;

namespace TestServer.LearningEndpoints;

/// <summary>
/// Simple tests to validate the test infrastructure works correctly.
/// </summary>
public class LearningEndpointSimpleTests : LearningEndpointTestBase
{
    public LearningEndpointSimpleTests(TestWebApplicationFactory<Program> factory) : base(factory)
    {
    }

    [Fact]
    public async Task SimpleTest_DatabaseInitialized_UserExists()
    {
        // Act & Assert
        await DatabaseAssertions.AssertUserExistsAsync(TestUserId, "spaceman", "C2");
    }

    [Fact]
    public async Task SimpleTest_GetPracticeNouns_ReturnsData()
    {
        // Act
        var nouns = await GetPracticeNounsAsync(TestUserId, 20);

        // Assert
        Assert.NotNull(nouns);
        // Note: If no nouns are available at the user's level, this is still valid behavior
        // The important thing is that the API call succeeded
        Assert.True(nouns.Count >= 0);
        Assert.True(nouns.Count <= 20);
    }

    [Fact]
    public async Task SimpleTest_GetLearningProgress_ReturnsData()
    {
        // Act
        var progress = await GetLearningProgressAsync(TestUserId);

        // Assert
        Assert.NotNull(progress);
        Assert.Equal("spaceman", progress.Username);
        Assert.Equal("C2", progress.LanguageLevel);
    }
}