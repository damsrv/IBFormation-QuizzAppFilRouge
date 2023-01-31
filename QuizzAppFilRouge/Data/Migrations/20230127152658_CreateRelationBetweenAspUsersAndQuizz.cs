using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizzAppFilRouge.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateRelationBetweenAspUsersAndQuizz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Quizzes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Notation = table.Column<double>(type: "float", nullable: true),
                    ValidationCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quizzes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Passages",
                columns: table => new
                {
                    QuizzId = table.Column<int>(type: "int", nullable: false),
                    IdentityUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PassageDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passages", x => new { x.QuizzId, x.IdentityUserId });
                    table.ForeignKey(
                        name: "FK_Passages_AspNetUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Passages_Quizzes_QuizzId",
                        column: x => x.QuizzId,
                        principalTable: "Quizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Passages_IdentityUserId",
                table: "Passages",
                column: "IdentityUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Passages");

            migrationBuilder.DropTable(
                name: "Quizzes");
        }
    }
}
