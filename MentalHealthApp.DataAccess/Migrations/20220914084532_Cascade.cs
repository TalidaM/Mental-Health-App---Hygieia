using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MentalHealthApp.DataAccess.Migrations
{
    public partial class Cascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UtilizatorId",
                table: "Programare");

            migrationBuilder.AddForeignKey(
                name: "FK_UtilizatorId",
                table: "Programare",
                column: "UtilizatorId",
                principalTable: "Utilizator",
                principalColumn: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UtilizatorId",
                table: "Programare");

            migrationBuilder.AddForeignKey(
                name: "FK_UtilizatorId",
                table: "Programare",
                column: "UtilizatorId",
                principalTable: "Utilizator",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
