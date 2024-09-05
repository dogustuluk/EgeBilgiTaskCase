using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EgeBilgiTaskCase.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Mig_10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CharacterDetails_CharacterId",
                table: "CharacterDetails",
                column: "CharacterId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterDetails_Characters_CharacterId",
                table: "CharacterDetails",
                column: "CharacterId",
                principalTable: "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharacterDetails_Characters_CharacterId",
                table: "CharacterDetails");

            migrationBuilder.DropIndex(
                name: "IX_CharacterDetails_CharacterId",
                table: "CharacterDetails");
        }
    }
}
