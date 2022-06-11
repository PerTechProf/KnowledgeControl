using Microsoft.EntityFrameworkCore.Migrations;

namespace KnowledgeControl.Migrations
{
    public partial class AnswersQuestions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TestQuestions",
                table: "Tests",
                newName: "Questions");

            migrationBuilder.RenameColumn(
                name: "TestAsnwers",
                table: "Tests",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "Answers",
                table: "Tests",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Answers",
                table: "Tests");

            migrationBuilder.RenameColumn(
                name: "Questions",
                table: "Tests",
                newName: "TestQuestions");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Tests",
                newName: "TestAsnwers");
        }
    }
}
