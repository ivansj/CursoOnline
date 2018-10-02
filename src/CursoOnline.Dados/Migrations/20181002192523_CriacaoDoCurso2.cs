using Microsoft.EntityFrameworkCore.Migrations;

namespace CursoOnline.Dados.Migrations
{
    public partial class CriacaoDoCurso2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "CargaHoraria",
                table: "Cursos",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Cursos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Cursos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PublicoAlvo",
                table: "Cursos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Valor",
                table: "Cursos",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CargaHoraria",
                table: "Cursos");

            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Cursos");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Cursos");

            migrationBuilder.DropColumn(
                name: "PublicoAlvo",
                table: "Cursos");

            migrationBuilder.DropColumn(
                name: "Valor",
                table: "Cursos");
        }
    }
}
