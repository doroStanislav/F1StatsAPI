using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace F1StatsAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddEntityRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "Teams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "DidNotFinish",
                table: "Results",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "GapToLeader",
                table: "Results",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Points",
                table: "Results",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "Results",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Time",
                table: "Results",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Drivers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Teams_CarId",
                table: "Teams",
                column: "CarId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_TeamId",
                table: "Drivers",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drivers_Teams_TeamId",
                table: "Drivers",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Cars_CarId",
                table: "Teams",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drivers_Teams_TeamId",
                table: "Drivers");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Cars_CarId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_CarId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Drivers_TeamId",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "DidNotFinish",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "GapToLeader",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "Points",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Drivers");
        }
    }
}
