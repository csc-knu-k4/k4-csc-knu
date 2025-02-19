using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OsvitaDAL.Migrations
{
    /// <inheritdoc />
    public partial class AddStatisticAssignmentsSetsAnswer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnswerId",
                table: "AssignmentProgressDetails");

            migrationBuilder.AddColumn<string>(
                name: "AnswerValue",
                table: "AssignmentProgressDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnswerValue",
                table: "AssignmentProgressDetails");

            migrationBuilder.AddColumn<int>(
                name: "AnswerId",
                table: "AssignmentProgressDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
