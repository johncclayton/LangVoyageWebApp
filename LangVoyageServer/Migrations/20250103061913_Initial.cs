using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LangVoyageServer.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Nouns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Noun = table.Column<string>(type: "TEXT", nullable: false),
                    Article = table.Column<string>(type: "TEXT", nullable: false),
                    Plural = table.Column<string>(type: "TEXT", nullable: true),
                    PluralArticle = table.Column<string>(type: "TEXT", nullable: true),
                    Level = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nouns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "varchar(50)", nullable: false),
                    LanguageLevel = table.Column<string>(type: "TEXT", nullable: true, defaultValue: "A1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NounProgresses",
                columns: table => new
                {
                    UserProfileId = table.Column<int>(type: "INTEGER", nullable: false),
                    NounId = table.Column<int>(type: "INTEGER", nullable: false),
                    TimeFrame = table.Column<int>(type: "INTEGER", nullable: false),
                    LastPractised = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NounProgresses", x => new { x.UserProfileId, x.NounId });
                    table.ForeignKey(
                        name: "FK_NounProgresses_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NounProgresses");

            migrationBuilder.DropTable(
                name: "Nouns");

            migrationBuilder.DropTable(
                name: "UserProfiles");
        }
    }
}
