using LangVoyageServer.Models;

namespace TestServer;

public class TestModels
{
    [Fact]
    public void LanguageNoun_PropertiesSetCorrectly()
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
    public void UserProfile_PropertiesSetCorrectly()
    {
        // Arrange & Act
        var user = new UserProfile
        {
            Id = 1,
            Username = "test_user",
            LanguageLevel = "B2"
        };

        // Assert
        Assert.Equal(1, user.Id);
        Assert.Equal("test_user", user.Username);
        Assert.Equal("B2", user.LanguageLevel);
    }

    [Fact]
    public void NounProgress_PropertiesSetCorrectly()
    {
        // Arrange
        var lastPractised = DateTime.UtcNow;

        // Act
        var progress = new NounProgress
        {
            UserProfileId = 1,
            NounId = 123,
            TimeFrame = 5,
            LastPractised = lastPractised
        };

        // Assert
        Assert.Equal(1, progress.UserProfileId);
        Assert.Equal(123, progress.NounId);
        Assert.Equal(5, progress.TimeFrame);
        Assert.Equal(lastPractised, progress.LastPractised);
    }

    [Fact]
    public void LanguageNounProgressView_PropertiesSetCorrectly()
    {
        // Arrange
        var lastPractised = DateTime.UtcNow;

        // Act
        var view = new LanguageNounProgressView
        {
            NounId = 123,
            Noun = "Auto",
            Article = "das",
            Plural = "Autos",
            PluralArticle = "die",
            NounLevel = "A2",
            UserProfileId = 1,
            Username = "test_user",
            UserLanguageLevel = "A2",
            TimeFrame = 3,
            LastPractised = lastPractised
        };

        // Assert
        Assert.Equal(123, view.NounId);
        Assert.Equal("Auto", view.Noun);
        Assert.Equal("das", view.Article);
        Assert.Equal("Autos", view.Plural);
        Assert.Equal("die", view.PluralArticle);
        Assert.Equal("A2", view.NounLevel);
        Assert.Equal(1, view.UserProfileId);
        Assert.Equal("test_user", view.Username);
        Assert.Equal("A2", view.UserLanguageLevel);
        Assert.Equal(3, view.TimeFrame);
        Assert.Equal(lastPractised, view.LastPractised);
    }

    [Theory]
    [InlineData("A1")]
    [InlineData("A2")]
    [InlineData("B1")]
    [InlineData("B2")]
    [InlineData("C1")]
    [InlineData("C2")]
    public void LanguageNoun_SupportsAllLanguageLevels(string level)
    {
        // Arrange & Act
        var noun = new LanguageNoun
        {
            Id = 1,
            Noun = "Test",
            Article = "der",
            Level = level
        };

        // Assert
        Assert.Equal(level, noun.Level);
    }

    [Theory]
    [InlineData("der")]
    [InlineData("die")]
    [InlineData("das")]
    public void LanguageNoun_SupportsAllArticles(string article)
    {
        // Arrange & Act
        var noun = new LanguageNoun
        {
            Id = 1,
            Noun = "Test",
            Article = article,
            Level = "A1"
        };

        // Assert
        Assert.Equal(article, noun.Article);
    }

    [Fact]
    public void NounProgress_TimeFrame_CanBeZero()
    {
        // Arrange & Act
        var progress = new NounProgress
        {
            UserProfileId = 1,
            NounId = 123,
            TimeFrame = 0,
            LastPractised = DateTime.UtcNow
        };

        // Assert
        Assert.Equal(0, progress.TimeFrame);
    }

    [Fact]
    public void NounProgress_TimeFrame_CanBePositive()
    {
        // Arrange & Act
        var progress = new NounProgress
        {
            UserProfileId = 1,
            NounId = 123,
            TimeFrame = 10,
            LastPractised = DateTime.UtcNow
        };

        // Assert
        Assert.Equal(10, progress.TimeFrame);
    }
}