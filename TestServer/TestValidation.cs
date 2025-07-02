using FluentValidation.TestHelper;
using LangVoyageServer.Requests;

namespace TestServer;

public class TestValidation
{
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