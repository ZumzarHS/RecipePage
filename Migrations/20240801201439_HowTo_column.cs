using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipePage.Migrations
{
    /// <inheritdoc />
    public partial class HowTo_column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HowToMake",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HowToMake",
                table: "Recipes");
        }
    }
}
