using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizzAppFilRouge.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateQuestionQuizzRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuestionQuizz",
                columns: table => new
                {
                    QuestionsId = table.Column<int>(type: "int", nullable: false),
                    QuizzesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionQuizz", x => new { x.QuestionsId, x.QuizzesId });
                    table.ForeignKey(
                        name: "FK_QuestionQuizz_Questions_QuestionsId",
                        column: x => x.QuestionsId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionQuizz_Quizzes_QuizzesId",
                        column: x => x.QuizzesId,
                        principalTable: "Quizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionQuizz_QuizzesId",
                table: "QuestionQuizz",
                column: "QuizzesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionQuizz");
        }
    }
}
