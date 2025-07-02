using LangVoyageServer.Models;
using LangVoyageServer.Requests;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace TestServer.UnitTests;

/// <summary>
/// Unit tests for model validation and constraints
/// </summary>
public class ModelTests
{
    [Fact]
    public void UserProfile_ShouldHaveCorrectProperties()
    {
        // Arrange & Act
        var userProfile = new UserProfile
        {
            Id = 1,
            Username = "testuser",
            LanguageLevel = "A1"
        };

        // Assert
        Assert.Equal(1, userProfile.Id);
        Assert.Equal("testuser", userProfile.Username);
        Assert.Equal("A1", userProfile.LanguageLevel);
        Assert.NotNull(userProfile.NounProgresses);
    }

    [Fact]
    public void LanguageNoun_ShouldHaveCorrectProperties()
    {
        // Arrange & Act
        var noun = new LanguageNoun
        {
            Id = 1,
            Noun = "Haus",
            Article = "das",
            Plural = "Häuser",
            PluralArticle = "die",
            Level = "A1"
        };

        // Assert
        Assert.Equal(1, noun.Id);
        Assert.Equal("Haus", noun.Noun);
        Assert.Equal("das", noun.Article);
        Assert.Equal("Häuser", noun.Plural);
        Assert.Equal("die", noun.PluralArticle);
        Assert.Equal("A1", noun.Level);
    }

    [Fact]
    public void NounProgress_ShouldHaveCorrectProperties()
    {
        // Arrange
        var dateTime = DateTime.UtcNow;

        // Act
        var progress = new NounProgress
        {
            UserProfileId = 1,
            NounId = 2,
            TimeFrame = 3,
            LastPractised = dateTime
        };

        // Assert
        Assert.Equal(1, progress.UserProfileId);
        Assert.Equal(2, progress.NounId);
        Assert.Equal(3, progress.TimeFrame);
        Assert.Equal(dateTime, progress.LastPractised);
    }

    [Fact]
    public void UpdateUserRequest_ShouldAllowNullValues()
    {
        // Arrange & Act
        var request = new UpdateUserRequest
        {
            Username = null,
            LanguageLevel = null
        };

        // Assert
        Assert.Null(request.Username);
        Assert.Null(request.LanguageLevel);
    }

    [Fact]
    public void NounProgressRequest_ShouldHaveCorrectProperties()
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

    [Theory]
    [InlineData(ValidLanguageLevel.A1)]
    [InlineData(ValidLanguageLevel.A2)]
    [InlineData(ValidLanguageLevel.B1)]
    [InlineData(ValidLanguageLevel.B2)]
    [InlineData(ValidLanguageLevel.C1)]
    [InlineData(ValidLanguageLevel.C2)]
    public void ValidLanguageLevel_ShouldContainAllExpectedValues(ValidLanguageLevel level)
    {
        // Act & Assert
        Assert.True(Enum.IsDefined(typeof(ValidLanguageLevel), level));
    }

    [Fact]
    public void ValidLanguageLevel_ShouldHaveExactlyExpectedValues()
    {
        // Act
        var values = Enum.GetValues<ValidLanguageLevel>();

        // Assert
        Assert.Equal(6, values.Length);
        Assert.Contains(ValidLanguageLevel.A1, values);
        Assert.Contains(ValidLanguageLevel.A2, values);
        Assert.Contains(ValidLanguageLevel.B1, values);
        Assert.Contains(ValidLanguageLevel.B2, values);
        Assert.Contains(ValidLanguageLevel.C1, values);
        Assert.Contains(ValidLanguageLevel.C2, values);
    }

    [Fact]
    public void LearningProgressResponse_ShouldHaveCorrectProperties()
    {
        // Arrange & Act
        var response = new LearningProgressResponse
        {
            Username = "testuser",
            LanguageLevel = "A1",
            TotalNouns = 100,
            NounProgresses = new int[] { 10, 20, 30 }
        };

        // Assert
        Assert.Equal("testuser", response.Username);
        Assert.Equal("A1", response.LanguageLevel);
        Assert.Equal(100, response.TotalNouns);
        Assert.NotNull(response.NounProgresses);
        Assert.Equal(3, response.NounProgresses.Length);
        Assert.Equal(10, response.NounProgresses[0]);
        Assert.Equal(20, response.NounProgresses[1]);
        Assert.Equal(30, response.NounProgresses[2]);
    }

    [Fact]
    public void UserProfile_ShouldInitializeNounProgressesCollection()
    {
        // Arrange & Act
        var userProfile = new UserProfile();

        // Assert
        Assert.NotNull(userProfile.NounProgresses);
        Assert.IsType<HashSet<NounProgress>>(userProfile.NounProgresses);
    }

    [Fact]
    public void LanguageNoun_ShouldAllowNullOptionalFields()
    {
        // Arrange & Act
        var noun = new LanguageNoun
        {
            Id = 1,
            Noun = "Test",
            Article = "das",
            Level = "A1"
            // Plural and PluralArticle not set (should be null)
        };

        // Assert
        Assert.Null(noun.Plural);
        Assert.Null(noun.PluralArticle);
    }
}