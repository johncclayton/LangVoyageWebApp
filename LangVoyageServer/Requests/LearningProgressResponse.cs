using System.ComponentModel.DataAnnotations;

namespace LangVoyageServer.Requests;

// the noun-progresses is just the count of nouns in each level.
public class LearningProgressResponse
{
    [Required]
    public required string Username { get; set; }
    [Required]
    public required string LanguageLevel { get; set; }
    [Required]
    public required int TotalNouns { get; set; }
    [Required]
    public required int[] NounProgresses { get; set; }    
}