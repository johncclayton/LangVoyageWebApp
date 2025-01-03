using LangVoyageServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LangVoyageServer.Database.EntityMapping.Sqlite;

public class NounProgressMapping : IEntityTypeConfiguration<NounProgress>
{
    public void Configure(EntityTypeBuilder<NounProgress> nounProgress)
    {
        nounProgress.Property(p => p.LastPractised)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}