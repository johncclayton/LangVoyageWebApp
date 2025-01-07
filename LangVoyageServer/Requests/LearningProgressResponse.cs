using System.ComponentModel.DataAnnotations;

namespace LangVoyageServer.Requests;

// the noun-progresses is just the count of nouns in each level.
public class LearningProgressResponse
{
    [Required]
    public required string Username { get; set; }
    [Required]
    public required string LanguageLevel { get; set; }
    
    /// <summary>
    /// The total number of nouns at this specific language level.  
    /// </summary>
    [Required]
    public required int TotalNouns { get; set; }
    
    /// <summary>
    /// The total number of nouns that the user has learned at this specific language level, split by level/time. 
    /// </summary>
    [Required]
    public required int[] NounProgresses { get; set; }    
}