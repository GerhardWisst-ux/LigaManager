using Microsoft.EntityFrameworkCore.Migrations;

namespace LigaManagement.Api.Migrations
{
    public partial class Vereine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vereine",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VereinNr = table.Column<int>(nullable: false),
                    Vereinsname1 = table.Column<string>(nullable: false),
                    Vereinsname2 = table.Column<string>(nullable: false),
                    Stadion = table.Column<string>(nullable: false),
                    Fassungsvermoegen = table.Column<string>(nullable: true),
                    Erfolge = table.Column<string>(nullable: true),
                    Gegruendet = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vereine", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vereine");
        }
    }
}
