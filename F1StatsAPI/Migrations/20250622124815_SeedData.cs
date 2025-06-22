using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace F1StatsAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "ChassisCode", "Name", "PoweUnit" },
                values: new object[] { 1, "SF-25", "Ferrari SF-25", "Ferrari" });

            migrationBuilder.InsertData(
                table: "GrandPrix",
                columns: new[] { "Id", "CircuitName", "Date", "Distance", "Laps", "Name" },
                values: new object[] { 1, "Albert Park Circuit", new DateTime(2025, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 306.12400000000002, 58, "Australian Grand Prix" });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "BaseLocation", "CarId", "FoundationYear", "Name", "TeamChief", "WorldChampionships" },
                values: new object[] { 1, "Maranello, Italy", 1, 1929, "Scuderia Ferrari", "Frédéric Vasseur", 16 });

            migrationBuilder.InsertData(
                table: "Drivers",
                columns: new[] { "Id", "Code", "Country", "DateOfBirth", "FamilyName", "GivenName", "Number", "TeamId" },
                values: new object[,]
                {
                    { 1, "LEC", "Monaco", new DateTime(1997, 10, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Leclerc", "Charles", 16, 1 },
                    { 2, "HAM", "United Kingdom", new DateTime(1985, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hamilton", "Lewis", 44, 1 }
                });

            migrationBuilder.InsertData(
                table: "Results",
                columns: new[] { "Id", "CarId", "DidNotFinish", "DriverId", "GapToLeader", "GrandPrixId", "Points", "Position", "TeamId", "Time" },
                values: new object[,]
                {
                    { 1, 1, false, 1, "+19.826s", 1, 4, 8, 1, null },
                    { 2, 1, false, 2, "+22.473s", 1, 1, 10, 1, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Results",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Results",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Drivers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "GrandPrix",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
