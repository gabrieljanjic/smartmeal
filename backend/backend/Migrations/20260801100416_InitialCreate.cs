using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealItems_Meals_MealId",
                table: "MealItems");

            migrationBuilder.DropTable(
                name: "Meals");

            migrationBuilder.DropIndex(
                name: "IX_MealItems_MealId",
                table: "MealItems");

            migrationBuilder.RenameColumn(
                name: "MealId",
                table: "MealItems",
                newName: "MealNumber");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "MealItems",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<int>(
                name: "DayId",
                table: "MealItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MealItems_DayId",
                table: "MealItems",
                column: "DayId");

            migrationBuilder.AddForeignKey(
                name: "FK_MealItems_Days_DayId",
                table: "MealItems",
                column: "DayId",
                principalTable: "Days",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MealItems_Days_DayId",
                table: "MealItems");

            migrationBuilder.DropIndex(
                name: "IX_MealItems_DayId",
                table: "MealItems");

            migrationBuilder.DropColumn(
                name: "DayId",
                table: "MealItems");

            migrationBuilder.RenameColumn(
                name: "MealNumber",
                table: "MealItems",
                newName: "MealId");

            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                table: "MealItems",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.CreateTable(
                name: "Meals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayId = table.Column<int>(type: "int", nullable: false),
                    MealNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meals_Days_DayId",
                        column: x => x.DayId,
                        principalTable: "Days",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MealItems_MealId",
                table: "MealItems",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_Meals_DayId",
                table: "Meals",
                column: "DayId");

            migrationBuilder.AddForeignKey(
                name: "FK_MealItems_Meals_MealId",
                table: "MealItems",
                column: "MealId",
                principalTable: "Meals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
