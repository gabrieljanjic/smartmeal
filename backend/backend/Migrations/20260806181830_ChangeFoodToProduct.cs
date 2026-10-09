using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class ChangeFoodToProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealItems_Foods_FoodId",
                table: "MealItems");

            migrationBuilder.DropTable(
                name: "Foods");

            migrationBuilder.RenameColumn(
                name: "FoodId",
                table: "MealItems",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_MealItems_FoodId",
                table: "MealItems",
                newName: "IX_MealItems_ProductId");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EanCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaloriesPer100 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProteinPer100 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FatPer100 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CarbsPer100 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_MealItems_Products_ProductId",
                table: "MealItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealItems_Products_ProductId",
                table: "MealItems");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "MealItems",
                newName: "FoodId");

            migrationBuilder.RenameIndex(
                name: "IX_MealItems_ProductId",
                table: "MealItems",
                newName: "IX_MealItems_FoodId");

            migrationBuilder.CreateTable(
                name: "Foods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AvgWeightG = table.Column<double>(type: "float", nullable: true),
                    CaloriesPer100 = table.Column<double>(type: "float", nullable: false),
                    CarbsPer100 = table.Column<double>(type: "float", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DefaultUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatPer100 = table.Column<double>(type: "float", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProteinPer100 = table.Column<double>(type: "float", nullable: false),
                    SupportsPiece = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foods", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_MealItems_Foods_FoodId",
                table: "MealItems",
                column: "FoodId",
                principalTable: "Foods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
