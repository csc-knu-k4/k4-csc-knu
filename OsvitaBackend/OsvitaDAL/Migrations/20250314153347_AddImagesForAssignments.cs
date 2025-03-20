using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OsvitaDAL.Migrations
{
    /// <inheritdoc />
    public partial class AddImagesForAssignments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProblemDescriptionImage",
                table: "Assignments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ValueImage",
                table: "Answers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EducationPlans_UserId",
                table: "EducationPlans",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EducationPlans_Users_UserId",
                table: "EducationPlans",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EducationPlans_Users_UserId",
                table: "EducationPlans");

            migrationBuilder.DropIndex(
                name: "IX_EducationPlans_UserId",
                table: "EducationPlans");

            migrationBuilder.DropColumn(
                name: "ProblemDescriptionImage",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "ValueImage",
                table: "Answers");
        }
    }
}
