using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FichaCadastroAPI.Migrations
{
    /// <inheritdoc />
    public partial class CorrectionDeleteCascate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Detalhe_Ficha_FichaId",
                table: "Detalhe");

            migrationBuilder.AddForeignKey(
                name: "FK_Detalhe_Ficha_FichaId",
                table: "Detalhe",
                column: "FichaId",
                principalTable: "Ficha",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Detalhe_Ficha_FichaId",
                table: "Detalhe");

            migrationBuilder.AddForeignKey(
                name: "FK_Detalhe_Ficha_FichaId",
                table: "Detalhe",
                column: "FichaId",
                principalTable: "Ficha",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
