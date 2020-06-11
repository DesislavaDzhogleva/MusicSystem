using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicSystem.Migrations
{
    public partial class MadeUniqueNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WriterDto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Pseudonym = table.Column<string>(maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WriterDto", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Writers_Name",
                table: "Writers",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Songs_Name",
                table: "Songs",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Producers_Name",
                table: "Producers",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Performers_StageName",
                table: "Performers",
                column: "StageName",
                unique: true,
                filter: "[StageName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Albums_Name",
                table: "Albums",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WriterDto");

            migrationBuilder.DropIndex(
                name: "IX_Writers_Name",
                table: "Writers");

            migrationBuilder.DropIndex(
                name: "IX_Songs_Name",
                table: "Songs");

            migrationBuilder.DropIndex(
                name: "IX_Producers_Name",
                table: "Producers");

            migrationBuilder.DropIndex(
                name: "IX_Performers_StageName",
                table: "Performers");

            migrationBuilder.DropIndex(
                name: "IX_Albums_Name",
                table: "Albums");
        }
    }
}
