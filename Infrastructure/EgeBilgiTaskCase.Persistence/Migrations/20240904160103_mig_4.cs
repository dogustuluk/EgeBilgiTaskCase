using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EgeBilgiTaskCase.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Locations_LocationId",
                table: "Characters");

            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Locations_OriginId",
                table: "Characters");

            migrationBuilder.DropIndex(
                name: "IX_Characters_LocationId",
                table: "Characters");

            migrationBuilder.DropIndex(
                name: "IX_Characters_OriginId",
                table: "Characters");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Characters_LocationId",
                table: "Characters",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_OriginId",
                table: "Characters",
                column: "OriginId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Locations_LocationId",
                table: "Characters",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Locations_OriginId",
                table: "Characters",
                column: "OriginId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
