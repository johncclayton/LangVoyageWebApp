using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace LangVoyageServer.Models;

[PrimaryKey("Id")]
public class LanguageNoun
{
    [Key]
    public int Id { get; set; }
    
    [Required, Key]
    public required string Noun { get; set; }

    [Required]
    public required string Article { get; set; }

    public string? Plural { get; set; }
    public string? PluralArticle { get; set; }
    
    [Required]
    public required string Level { get; set; }
}