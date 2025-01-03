
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace LangVoyageServer.Models;

[PrimaryKey(nameof(UserProfileId), nameof(NounId))]
public class NounProgress
{
    [Required]
    public int UserProfileId { get; set; }
    
    [Required]
    public int NounId { get; set; }

    [Required]
    public int TimeFrame { get; set; }
    
    public DateTime LastPractised { get; set; }
}