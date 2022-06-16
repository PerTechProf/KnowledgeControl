using Microsoft.EntityFrameworkCore.Migrations;

namespace KnowledgeControl.Migrations
{
    public partial class ConstraintsFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Solutions_AspNetUsers_UserId",
                table: "Solutions");

            migrationBuilder.AddForeignKey(
                name: "FK_Solutions_AspNetUsers_UserId",
                table: "Solutions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Solutions_AspNetUsers_UserId",
                table: "Solutions");

            migrationBuilder.AddForeignKey(
                name: "FK_Solutions_AspNetUsers_UserId",
                table: "Solutions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
