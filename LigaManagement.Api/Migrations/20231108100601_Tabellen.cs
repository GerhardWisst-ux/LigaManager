using Microsoft.EntityFrameworkCore.Migrations;

namespace LigaManagement.Api.Migrations
{
    public partial class Tabellen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tabellen",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VereinNr = table.Column<int>(nullable: false),
                    Verein = table.Column<string>(nullable: false),
                    Tab_Sai_Id = table.Column<int>(nullable: false),
                    Liga = table.Column<string>(nullable: false),
                    Tab_Lig_Id = table.Column<string>(nullable: true),
                    Platz = table.Column<int>(nullable: false),
                    Spiele = table.Column<int>(nullable: false),
                    Punkte = table.Column<int>(nullable: false),
                    Gewonnen = table.Column<int>(nullable: false),
                    Untentschieden = table.Column<int>(nullable: false),
                    Verloren = table.Column<int>(nullable: false),
                    TorePlus = table.Column<int>(nullable: false),
                    ToreMinus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tabellen", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tabellen");
        }
    }
}
