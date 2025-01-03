using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LangVoyageServer.Migrations
{
    /// <inheritdoc />
    public partial class CreateLanguageNounProgressView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE VIEW NounProgressView AS
                SELECT 
                    ln.Id AS NounId,
                    ln.Noun,
                    ln.Article,
                    ln.Plural,
                    ln.PluralArticle,
                    ln.Level AS NounLevel,
                    up.Id AS UserProfileId,
                    up.Username,
                    up.LanguageLevel AS UserLanguageLevel,
                    COALESCE(np.TimeFrame, 0) AS TimeFrame,
                    COALESCE(np.LastPractised, '1970-01-01') AS LastPractised
                FROM 
                    Nouns ln
                CROSS JOIN
                    UserProfiles up
                LEFT JOIN 
                    NounProgresses np ON ln.Id = np.NounId AND up.Id = np.UserProfileId
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW NounProgressView");
        }
    }
}
