using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Foods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CaloriesPer100 = table.Column<double>(type: "float", nullable: false),
                    ProteinPer100 = table.Column<double>(type: "float", nullable: false),
                    FatPer100 = table.Column<double>(type: "float", nullable: false),
                    CarbsPer100 = table.Column<double>(type: "float", nullable: false),
                    DefaultUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupportsPiece = table.Column<bool>(type: "bit", nullable: false),
                    AvgWeightG = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foods", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Foods");
        }
    }
}
