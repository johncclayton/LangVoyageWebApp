using FluentValidation.TestHelper;
using LangVoyageServer.Requests;

namespace TestServer;

/// <summary>
/// Tests for validation logic using FluentValidation
/// </summary>
public class ValidationTests
{
    private readonly UpdateUserRequestValidator _validator;

    public ValidationTests()
    {
        _validator = new UpdateUserRequestValidator();
    }

    [Theory]
    [InlineData("A1")]
    [InlineData("A2")]
    [InlineData("B1")]
    [InlineData("B2")]
    [InlineData("C1")]
    [InlineData("C2")]
    [InlineData("a1")] // Test case insensitivity
    [InlineData("c2")]
    public void UpdateUserRequestValidator_WithValidLanguageLevel_PassesValidation(string languageLevel)
    {
        // Arrange
        var request = new UpdateUserRequest { LanguageLevel = languageLevel };

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.LanguageLevel);
    }

    [Theory]
    [InlineData("A3")]
    [InlineData("D1")]
    [InlineData("Z9")]
    [InlineData("InvalidLevel")]
    [InlineData("123")]
    [InlineData("")]
    [InlineData("   ")]
    public void UpdateUserRequestValidator_WithInvalidLanguageLevel_FailsValidation(string languageLevel)
    {
        // Arrange
        var request = new UpdateUserRequest { LanguageLevel = languageLevel };

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.LanguageLevel);
    }

    [Fact]
    public void UpdateUserRequestValidator_WithNullLanguageLevel_PassesValidation()
    {
        // Arrange
        var request = new UpdateUserRequest { LanguageLevel = null };

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.LanguageLevel);
    }

    [Theory]
    [InlineData("ValidUsername")]
    [InlineData("User123")]
    [InlineData("test_user")]
    [InlineData(null)] // Username can be null for partial updates
    public void UpdateUserRequestValidator_WithValidUsername_PassesValidation(string? username)
    {
        // Arrange
        var request = new UpdateUserRequest { Username = username };

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Username);
    }

    [Fact]
    public void UpdateUserRequestValidator_WithBothFieldsValid_PassesValidation()
    {
        // Arrange
        var request = new UpdateUserRequest 
        { 
            Username = "TestUser",
            LanguageLevel = "B2"
        };

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void UpdateUserRequestValidator_WithBothFieldsNull_PassesValidation()
    {
        // Arrange
        var request = new UpdateUserRequest 
        { 
            Username = null,
            LanguageLevel = null
        };

        // Act
        var result = _validator.TestValidate(request);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void ValidLanguageLevel_Enum_ContainsExpectedValues()
    {
        // This test ensures the enum has the expected values
        var expectedValues = new[] { "A1", "A2", "B1", "B2", "C1", "C2" };
        var enumValues = Enum.GetNames(typeof(ValidLanguageLevel));

        // Assert
        Assert.Equal(expectedValues.Length, enumValues.Length);
        foreach (var expected in expectedValues)
        {
            Assert.Contains(expected, enumValues);
        }
    }

    [Theory]
    [InlineData("A1", ValidLanguageLevel.A1)]
    [InlineData("A2", ValidLanguageLevel.A2)]
    [InlineData("B1", ValidLanguageLevel.B1)]
    [InlineData("B2", ValidLanguageLevel.B2)]
    [InlineData("C1", ValidLanguageLevel.C1)]
    [InlineData("C2", ValidLanguageLevel.C2)]
    public void ValidLanguageLevel_Enum_ParsesCorrectly(string stringValue, ValidLanguageLevel expectedEnum)
    {
        // Act
        var success = Enum.TryParse<ValidLanguageLevel>(stringValue, true, out var result);

        // Assert
        Assert.True(success);
        Assert.Equal(expectedEnum, result);
    }
}