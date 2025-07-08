using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace F1StatsAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddPowerUnitCar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PoweUnit",
                table: "Cars",
                newName: "PowerUnit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PowerUnit",
                table: "Cars",
                newName: "PoweUnit");
        }
    }
}
