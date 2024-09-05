using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EgeBilgiTaskCase.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Mig_7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dimension",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Locations");

            migrationBuilder.AddColumn<int>(
                name: "DimensionId",
                table: "Locations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Locations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DimensionId",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Locations");

            migrationBuilder.AddColumn<string>(
                name: "Dimension",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
