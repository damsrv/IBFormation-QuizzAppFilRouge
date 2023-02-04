using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizzAppFilRouge.Data.Migrations
{
    /// <inheritdoc />
    public partial class QuizzCreatorKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AddColumn<string>(
                name: "QuizzCreatorId",
                table: "Quizzes",
                type: "nvarchar(450)",
                nullable: true);


            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_QuizzCreatorId",
                table: "Quizzes",
                column: "QuizzCreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quizzes_AspNetUsers_QuizzCreatorId",
                table: "Quizzes",
                column: "QuizzCreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quizzes_AspNetUsers_QuizzCreatorId",
                table: "Quizzes");

         
            migrationBuilder.DropIndex(
                name: "IX_Quizzes_QuizzCreatorId",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "QuizzCreatorId",
                table: "Quizzes");



          



        }
    }
}
