using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizzAppFilRouge.Data.Migrations
{
    /// <inheritdoc />
    public partial class AjoutColonneIsCorrectDansTableResponse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            


            migrationBuilder.AddColumn<bool>(
                name: "IsCorrect",
                table: "Responses",
                type: "bit",
                nullable: false,
                defaultValue: null);

           
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.DropColumn(
                name: "IsCorrect",
                table: "Responses");

           
        }
    }
}
