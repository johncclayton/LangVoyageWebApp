using LangVoyageServer.Database.EntityMapping.Sqlite;
using LangVoyageServer.Models;
using Microsoft.EntityFrameworkCore;

namespace LangVoyageServer.Database;

public class LangServerDbContext(DbContextOptions<LangServerDbContext> options) : DbContext(options)
{
    // raw data, stuff that has nothing to do with the user and exercises.
    public DbSet<LanguageNoun> Nouns { get; set; }
    
    public DbSet<UserProfile> UserProfiles { get; set; }
    
    public DbSet<NounProgress> NounProgresses { get; set; }
    
    public DbSet<LanguageNounProgressView> NounProgressView { get; set; } 
    
    // a user practises many different nouns, which ones went well, which ones do they
    // need help with.  when did they practise? 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration(new UserProfileMapping());
        modelBuilder.ApplyConfiguration(new NounProgressMapping());
        
        modelBuilder.Entity<LanguageNounProgressView>().ToView("NounProgressView").HasNoKey();
    }
}