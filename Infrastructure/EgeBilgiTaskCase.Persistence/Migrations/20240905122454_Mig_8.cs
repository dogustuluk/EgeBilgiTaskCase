using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EgeBilgiTaskCase.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Mig_8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ApiCreatedDate",
                table: "Locations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LocationApiId",
                table: "Locations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Residents",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApiCreatedDate",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "LocationApiId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "Residents",
                table: "Locations");
        }
    }
}
