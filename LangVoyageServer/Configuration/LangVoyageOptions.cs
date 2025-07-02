namespace LangVoyageServer.Configuration;

/// <summary>
/// Configuration options for the Language Voyage application.
/// </summary>
public class LangVoyageOptions
{
    public const string SectionName = "LangVoyage";

    /// <summary>
    /// Database configuration options.
    /// </summary>
    public DatabaseOptions Database { get; set; } = new();

    /// <summary>
    /// CORS configuration options.
    /// </summary>
    public CorsOptions Cors { get; set; } = new();

    /// <summary>
    /// Default user configuration for seeding.
    /// </summary>
    public DefaultUserOptions DefaultUser { get; set; } = new();
}

public class DatabaseOptions
{
    /// <summary>
    /// Default number of nouns to return in practice sessions.
    /// </summary>
    public int DefaultNounLimit { get; set; } = 20;

    /// <summary>
    /// Maximum number of nouns that can be requested at once.
    /// </summary>
    public int MaxNounLimit { get; set; } = 10000;
}

public class CorsOptions
{
    /// <summary>
    /// List of allowed origins for CORS policy.
    /// </summary>
    public string[] AllowedOrigins { get; set; } = { "*" };

    /// <summary>
    /// Whether to allow any method in CORS policy.
    /// </summary>
    public bool AllowAnyMethod { get; set; } = true;

    /// <summary>
    /// Whether to allow any header in CORS policy.
    /// </summary>
    public bool AllowAnyHeader { get; set; } = true;
}

public class DefaultUserOptions
{
    /// <summary>
    /// Default username for seeding.
    /// </summary>
    public string Username { get; set; } = "johnclayton";

    /// <summary>
    /// Default language level for seeding.
    /// </summary>
    public string LanguageLevel { get; set; } = "B2";
}