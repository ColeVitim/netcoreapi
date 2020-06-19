using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace APIcomSQLITE.Migrations
{
    public partial class BancoInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "palavras",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nome = table.Column<string>(nullable: true),
                    pontuacao = table.Column<int>(nullable: false),
                    ativo = table.Column<bool>(nullable: false),
                    criado = table.Column<DateTime>(nullable: false),
                    atualizado = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_palavras", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "palavras");
        }
    }
}
