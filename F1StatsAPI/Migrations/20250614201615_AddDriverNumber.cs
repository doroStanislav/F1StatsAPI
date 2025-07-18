﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace F1StatsAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddDriverNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Drivers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "Drivers");
        }
    }
}
