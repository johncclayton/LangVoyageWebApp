using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LangVoyageServer.Models;

public class UserProfile
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "varchar(50)")]
    public string? Username { get; set; }
    
    public string? LanguageLevel { get; set; }
    
    public ICollection<NounProgress>? NounProgresses { get; set; } = new HashSet<NounProgress>();
}