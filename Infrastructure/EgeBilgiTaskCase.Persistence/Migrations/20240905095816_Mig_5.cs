using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EgeBilgiTaskCase.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Mig_5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EpisodeIds",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "OriginId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Species",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Characters");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EpisodeIds",
                table: "Characters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Characters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OriginId",
                table: "Characters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Species",
                table: "Characters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Characters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Characters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
