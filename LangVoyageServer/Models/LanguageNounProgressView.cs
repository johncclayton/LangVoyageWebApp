using System.ComponentModel.DataAnnotations.Schema;

namespace LangVoyageServer.Models;

[Table("NounProgressView")]
public class LanguageNounProgressView
{
    public int NounId { get; set; }
    public required string Noun { get; set; }
    public required string Article { get; set; }
    public required string? Plural { get; set; }
    public string? PluralArticle { get; set; }
    public required string NounLevel { get; set; }
    public int UserProfileId { get; set; }
    public required string Username { get; set; }
    public required string UserLanguageLevel { get; set; }
    public int TimeFrame { get; set; }
    public DateTime LastPractised { get; set; }
}