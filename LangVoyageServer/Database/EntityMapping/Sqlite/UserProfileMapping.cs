using LangVoyageServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LangVoyageServer.Database.EntityMapping.Sqlite;

public class UserProfileMapping : IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> userProfile)
    {
        userProfile.HasKey(u => u.Id);
        userProfile.Property(c => c.LanguageLevel).HasDefaultValue("A1");
        userProfile.HasMany(profile => profile.NounProgresses);
    }
    
}