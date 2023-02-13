using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizzAppFilRouge.Data.Migrations
{
    /// <inheritdoc />
    public partial class AjoutPossibilitéDeGererLesLangages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuizzLangage",
                table: "Quizzes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuestionLangage",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.DropColumn(
                name: "QuizzLangage",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "QuestionLangage",
                table: "Questions");
        }
    }
}
