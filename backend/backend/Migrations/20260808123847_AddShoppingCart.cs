using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddShoppingCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unit",
                table: "MealItems");

            migrationBuilder.AddColumn<bool>(
                name: "isBought",
                table: "MealItems",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isBought",
                table: "MealItems");

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "MealItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
