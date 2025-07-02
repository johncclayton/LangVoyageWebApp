using FluentValidation.TestHelper;
using LangVoyageServer.Requests;

namespace TestServer;

public class TestValidation
{
    /// <summary>
    /// Verifies that UpdateUserRequestValidator accepts valid language levels without validation errors.
    /// This test ensures basic validation functionality works for standard A1-C2 language level inputs.
    /// </summary>
    [Fact]
    public void UpdateUserRequestValidator_WithValidLanguageLevel_PassesValidation()
    {
        // Arrange
        var validator = new UpdateUserRequestValidator();
        var request = new UpdateUserRequest
        {
            Username = "test_user",
            LanguageLevel = "A1"
        };

        // Act
        var result = validator.TestValidate(request);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Verifies that UpdateUserRequestValidator accepts all valid CEFR language levels (A1-C2).
    /// This parameterized test ensures comprehensive coverage of the European language proficiency framework standards.
    /// </summary>
    [Theory]
    [InlineData("A1")]
    [InlineData("A2")]
    [InlineData("B1")]
    [InlineData("B2")]
    [InlineData("C1")]
    [InlineData("C2")]
    public void UpdateUserRequestValidator_WithValidLanguageLevels_PassesValidation(string languageLevel)
    {
        // Arrange
        var validator = new UpdateUserRequestValidator();
        var request = new UpdateUserRequest
        {
            Username = "test_user",
            LanguageLevel = languageLevel
        };

        // Act
        var result = validator.TestValidate(request);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Verifies that UpdateUserRequestValidator rejects invalid language level values and empty strings.
    /// This parameterized test ensures proper input validation prevents malformed data from entering the system.
    /// </summary>
    [Theory]
    [InlineData("C0")]
    [InlineData("D1")]
    [InlineData("INVALID")]
    [InlineData("")]
    [InlineData(" ")]
    public void UpdateUserRequestValidator_WithInvalidLanguageLevel_FailsValidation(string invalidLanguageLevel)
    {
        // Arrange
        var validator = new UpdateUserRequestValidator();
        var request = new UpdateUserRequest
        {
            Username = "test_user",
            LanguageLevel = invalidLanguageLevel
        };

        // Act
        var result = validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.LanguageLevel);
    }

    /// <summary>
    /// Verifies that UpdateUserRequestValidator allows null language level values for optional updates.
    /// This test ensures partial profile updates are supported without requiring all fields to be specified.
    /// </summary>
    [Fact]
    public void UpdateUserRequestValidator_WithNullLanguageLevel_PassesValidation()
    {
        // Arrange
        var validator = new UpdateUserRequestValidator();
        var request = new UpdateUserRequest
        {
            Username = "test_user",
            LanguageLevel = null
        };

        // Act
        var result = validator.TestValidate(request);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Verifies that UpdateUserRequestValidator accepts language levels in both uppercase and lowercase formats.
    /// This parameterized test ensures case-insensitive validation for better user experience and data consistency.
    /// </summary>
    [Theory]
    [InlineData("a1")]  // lowercase
    [InlineData("A1")]  // uppercase
    [InlineData("c1")]  // lowercase
    [InlineData("C1")]  // uppercase
    public void UpdateUserRequestValidator_WithCaseInsensitiveLanguageLevel_PassesValidation(string languageLevel)
    {
        // Arrange
        var validator = new UpdateUserRequestValidator();
        var request = new UpdateUserRequest
        {
            Username = "test_user",
            LanguageLevel = languageLevel
        };

        // Act
        var result = validator.TestValidate(request);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Verifies that NounProgressRequest model properties can be set and retrieved correctly.
    /// This test ensures the request model maintains data integrity for noun practice progress tracking.
    /// </summary>
    [Fact]
    public void NounProgressRequest_PropertiesSetCorrectly()
    {
        // Arrange & Act
        var request = new NounProgressRequest
        {
            NounId = 123,
            AnswerWasCorrect = true
        };

        // Assert
        Assert.Equal(123, request.NounId);
        Assert.True(request.AnswerWasCorrect);
    }

    /// <summary>
    /// Verifies that LearningProgressResponse model properties can be set and retrieved correctly.
    /// This test ensures the response model maintains data integrity for learning progress dashboard functionality.
    /// </summary>
    [Fact]
    public void LearningProgressResponse_PropertiesSetCorrectly()
    {
        // Arrange & Act
        var response = new LearningProgressResponse
        {
            Username = "test_user",
            LanguageLevel = "B1",
            TotalNouns = 100,
            NounProgresses = new[] { 10, 20, 30 }
        };

        // Assert
        Assert.Equal("test_user", response.Username);
        Assert.Equal("B1", response.LanguageLevel);
        Assert.Equal(100, response.TotalNouns);
        Assert.Equal(new[] { 10, 20, 30 }, response.NounProgresses);
    }
}