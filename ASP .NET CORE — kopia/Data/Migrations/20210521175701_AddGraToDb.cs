using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Data.Migrations
{
    public partial class AddGraToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gra",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tytul = table.Column<string>(nullable: false),
                    Firma = table.Column<string>(nullable: false),
                    Gatunek = table.Column<string>(nullable: false),
                    Ocena = table.Column<int>(nullable: false)

                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gra", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Gra");
        }
    }
}
