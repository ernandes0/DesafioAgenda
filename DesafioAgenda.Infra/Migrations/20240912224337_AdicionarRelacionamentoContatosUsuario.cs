using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DesafioAgenda.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarRelacionamentoContatosUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Contatos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Contatos_UserId",
                table: "Contatos",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contatos_Users_UserId",
                table: "Contatos",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contatos_Users_UserId",
                table: "Contatos");

            migrationBuilder.DropIndex(
                name: "IX_Contatos_UserId",
                table: "Contatos");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Contatos");
        }
    }
}
