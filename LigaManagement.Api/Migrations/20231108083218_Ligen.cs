using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LigaManagement.Api.Migrations
{
    public partial class Ligen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ligen",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Liganame = table.Column<string>(nullable: false),
                    Verband = table.Column<string>(nullable: false),
                    Erstaustragung = table.Column<DateTime>(nullable: false),
                    Absteiger = table.Column<int>(nullable: false),
                    Aktiv = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ligen", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ligen");
        }
    }
}
