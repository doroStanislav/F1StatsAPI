using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace F1StatsAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddIsActiveToDriver : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Drivers",
                type: "bit",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsActive",
                value: null);

            migrationBuilder.UpdateData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsActive",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Drivers");
        }
    }
}
