using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LigaManagement.Api.Migrations
{
    public partial class Spieltage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Spieltage",
                columns: table => new
                {
                    SpieltagId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpieltagNr = table.Column<string>(nullable: false),
                    Saison = table.Column<string>(nullable: false),
                    SaisonID = table.Column<int>(nullable: false),
                    LigaID = table.Column<int>(nullable: false),
                    Verein1_Nr = table.Column<string>(nullable: false),
                    Verein1 = table.Column<string>(nullable: false),
                    Verein2_Nr = table.Column<string>(nullable: false),
                    Verein2 = table.Column<string>(nullable: false),
                    Tore1_Nr = table.Column<int>(nullable: false),
                    Tore2_Nr = table.Column<int>(nullable: false),
                    Datum = table.Column<DateTime>(nullable: false),
                    Ort = table.Column<string>(nullable: false),
                    Schiedrichter = table.Column<string>(nullable: true),
                    Abgeschlossen = table.Column<bool>(nullable: false),
                    Zuschauer = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spieltage", x => x.SpieltagId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Spieltage");
        }
    }
}
