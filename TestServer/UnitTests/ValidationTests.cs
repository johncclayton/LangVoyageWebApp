using FluentValidation;
using LangVoyageServer.Requests;
using Xunit;

namespace TestServer.UnitTests;

/// <summary>
/// Unit tests for validation logic to ensure proper input validation
/// </summary>
public class ValidationTests
{
    private readonly UpdateUserRequestValidator _validator;

    public ValidationTests()
    {
        _validator = new UpdateUserRequestValidator();
    }

    [Fact]
    public async Task UpdateUserRequestValidator_ShouldNotHaveError_WhenLanguageLevelIsNull()
    {
        // Arrange
        var request = new UpdateUserRequest { LanguageLevel = null, Username = "test" };

        // Act
        var result = await _validator.ValidateAsync(request);

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors.Where(e => e.PropertyName == nameof(request.LanguageLevel)));
    }

    [Theory]
    [InlineData("A1")]
    [InlineData("A2")]
    [InlineData("B1")]
    [InlineData("B2")]
    [InlineData("C1")]
    [InlineData("C2")]
    public async Task UpdateUserRequestValidator_ShouldNotHaveError_WhenLanguageLevelIsValid(string languageLevel)
    {
        // Arrange
        var request = new UpdateUserRequest { LanguageLevel = languageLevel, Username = "test" };

        // Act
        var result = await _validator.ValidateAsync(request);

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors.Where(e => e.PropertyName == nameof(request.LanguageLevel)));
    }

    [Theory]
    [InlineData("a1")] // lowercase
    [InlineData("a2")]
    [InlineData("b1")]
    [InlineData("b2")]
    [InlineData("c1")]
    [InlineData("c2")]
    public async Task UpdateUserRequestValidator_ShouldNotHaveError_WhenLanguageLevelIsValidLowercase(string languageLevel)
    {
        // Arrange
        var request = new UpdateUserRequest { LanguageLevel = languageLevel, Username = "test" };

        // Act
        var result = await _validator.ValidateAsync(request);

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors.Where(e => e.PropertyName == nameof(request.LanguageLevel)));
    }

    [Theory]
    [InlineData("A0")]
    [InlineData("A3")]
    [InlineData("B0")]
    [InlineData("B3")]
    [InlineData("C0")]
    [InlineData("C3")]
    [InlineData("D1")]
    [InlineData("")]
    [InlineData("invalid")]
    [InlineData("Z1")]
    public async Task UpdateUserRequestValidator_ShouldHaveError_WhenLanguageLevelIsInvalid(string languageLevel)
    {
        // Arrange
        var request = new UpdateUserRequest { LanguageLevel = languageLevel, Username = "test" };

        // Act
        var result = await _validator.ValidateAsync(request);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => 
            e.PropertyName == nameof(request.LanguageLevel) && 
            e.ErrorMessage == "The provided value is not a valid enum value.");
    }

    [Fact]
    public async Task UpdateUserRequestValidator_ShouldNotHaveError_WhenUsernameIsNull()
    {
        // Arrange
        var request = new UpdateUserRequest { Username = null, LanguageLevel = "A1" };

        // Act
        var result = await _validator.ValidateAsync(request);

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors.Where(e => e.PropertyName == nameof(request.Username)));
    }

    [Fact]
    public async Task UpdateUserRequestValidator_ShouldNotHaveError_WhenUsernameIsValid()
    {
        // Arrange
        var request = new UpdateUserRequest { Username = "validuser", LanguageLevel = "A1" };

        // Act
        var result = await _validator.ValidateAsync(request);

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors.Where(e => e.PropertyName == nameof(request.Username)));
    }

    [Fact]
    public async Task UpdateUserRequestValidator_ShouldNotHaveError_WhenBothFieldsAreNull()
    {
        // Arrange
        var request = new UpdateUserRequest { Username = null, LanguageLevel = null };

        // Act
        var result = await _validator.ValidateAsync(request);

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public async Task UpdateUserRequestValidator_ShouldNotHaveError_WhenBothFieldsAreValid()
    {
        // Arrange
        var request = new UpdateUserRequest { Username = "validuser", LanguageLevel = "B2" };

        // Act
        var result = await _validator.ValidateAsync(request);

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }
}