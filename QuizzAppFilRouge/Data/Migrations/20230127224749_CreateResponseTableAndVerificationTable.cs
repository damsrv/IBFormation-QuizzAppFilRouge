using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizzAppFilRouge.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateResponseTableAndVerificationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Response",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Response", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Verification",
                columns: table => new
                {
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    IdentityUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ResponseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Verification", x => new { x.QuestionId, x.IdentityUserId, x.ResponseId });
                    table.ForeignKey(
                        name: "FK_Verification_AspNetUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Verification_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Verification_Response_ResponseId",
                        column: x => x.ResponseId,
                        principalTable: "Response",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Verification_IdentityUserId",
                table: "Verification",
                column: "IdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Verification_ResponseId",
                table: "Verification",
                column: "ResponseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Verification");

            migrationBuilder.DropTable(
                name: "Response");
        }
    }
}
