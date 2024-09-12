using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipePage.Migrations
{
    /// <inheritdoc />
    public partial class Instructions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HowToMake",
                table: "Recipes",
                newName: "Instructions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Instructions",
                table: "Recipes",
                newName: "HowToMake");
        }
    }
}
