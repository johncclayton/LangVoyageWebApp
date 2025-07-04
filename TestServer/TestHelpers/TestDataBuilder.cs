using LangVoyageServer.Models;
using LangVoyageServer.Requests;

namespace TestServer.TestHelpers;

/// <summary>
/// Builder class for creating test data objects with fluent API.
/// Provides default values and allows customization for testing scenarios.
/// </summary>
public class TestDataBuilder
{
    /// <summary>
    /// Creates a builder for UserProfile test data.
    /// </summary>
    public static UserProfileBuilder UserProfile() => new();
    
    /// <summary>
    /// Creates a builder for LanguageNoun test data.
    /// </summary>
    public static LanguageNounBuilder LanguageNoun() => new();
    
    /// <summary>
    /// Creates a builder for NounProgress test data.
    /// </summary>
    public static NounProgressBuilder NounProgress() => new();
    
    /// <summary>
    /// Creates a builder for NounProgressRequest test data.
    /// </summary>
    public static NounProgressRequestBuilder NounProgressRequest() => new();
    
    /// <summary>
    /// Creates a builder for UpdateUserRequest test data.
    /// </summary>
    public static UpdateUserRequestBuilder UpdateUserRequest() => new();
}

public class UserProfileBuilder
{
    private UserProfile _user = new()
    {
        Id = 1,
        Username = "test-user",
        LanguageLevel = "B2"
    };

    public UserProfileBuilder WithId(int id)
    {
        _user.Id = id;
        return this;
    }

    public UserProfileBuilder WithUsername(string username)
    {
        _user.Username = username;
        return this;
    }

    public UserProfileBuilder WithLanguageLevel(string level)
    {
        _user.LanguageLevel = level;
        return this;
    }

    public UserProfile Build() => _user;
}

public class LanguageNounBuilder
{
    private LanguageNoun _noun = new()
    {
        Id = 1,
        Noun = "Haus",
        Level = "B2",
        Article = "das"
    };

    public LanguageNounBuilder WithId(int id)
    {
        _noun.Id = id;
        return this;
    }

    public LanguageNounBuilder WithNoun(string noun)
    {
        _noun.Noun = noun;
        return this;
    }

    public LanguageNounBuilder WithLevel(string level)
    {
        _noun.Level = level;
        return this;
    }

    public LanguageNounBuilder WithArticle(string article)
    {
        _noun.Article = article;
        return this;
    }

    public LanguageNounBuilder WithPlural(string? plural)
    {
        _noun.Plural = plural;
        return this;
    }

    public LanguageNounBuilder WithPluralArticle(string? pluralArticle)
    {
        _noun.PluralArticle = pluralArticle;
        return this;
    }

    public LanguageNoun Build() => _noun;
}

public class NounProgressBuilder
{
    private NounProgress _progress = new()
    {
        UserProfileId = 1,
        NounId = 1,
        TimeFrame = 1,
        LastPractised = DateTime.UtcNow
    };

    public NounProgressBuilder WithUserProfileId(int userId)
    {
        _progress.UserProfileId = userId;
        return this;
    }

    public NounProgressBuilder WithNounId(int nounId)
    {
        _progress.NounId = nounId;
        return this;
    }

    public NounProgressBuilder WithTimeFrame(int timeFrame)
    {
        _progress.TimeFrame = timeFrame;
        return this;
    }

    public NounProgressBuilder WithLastPractised(DateTime lastPractised)
    {
        _progress.LastPractised = lastPractised;
        return this;
    }

    public NounProgress Build() => _progress;
}

public class NounProgressRequestBuilder
{
    private NounProgressRequest _request = new()
    {
        NounId = 1,
        AnswerWasCorrect = true
    };

    public NounProgressRequestBuilder WithNounId(int nounId)
    {
        _request.NounId = nounId;
        return this;
    }

    public NounProgressRequestBuilder WithAnswerWasCorrect(bool wasCorrect)
    {
        _request.AnswerWasCorrect = wasCorrect;
        return this;
    }

    public NounProgressRequest Build() => _request;
}

public class UpdateUserRequestBuilder
{
    private UpdateUserRequest _request = new()
    {
        Username = "test-user",
        LanguageLevel = "B2"
    };

    public UpdateUserRequestBuilder WithUsername(string username)
    {
        _request.Username = username;
        return this;
    }

    public UpdateUserRequestBuilder WithLanguageLevel(string level)
    {
        _request.LanguageLevel = level;
        return this;
    }

    public UpdateUserRequest Build() => _request;
}