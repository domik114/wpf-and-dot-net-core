﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace KinoBase.Data.Migrations
{
    public partial class addFilmToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Film",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tytul = table.Column<string>(nullable: false),
                    Rok = table.Column<int>(nullable: false),
                    Gatunek = table.Column<string>(nullable: false),
                    Rezyser = table.Column<string>(nullable: false),
                    Kraj = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Film", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Film");
        }
    }
}
