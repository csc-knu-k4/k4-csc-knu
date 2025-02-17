using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OsvitaDAL.Migrations
{
    /// <inheritdoc />
    public partial class AddStatisticAssignmentsSets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnswerId",
                table: "AssignmentSetProgressDetails");

            migrationBuilder.RenameColumn(
                name: "AssignmentId",
                table: "AssignmentSetProgressDetails",
                newName: "Score");

            migrationBuilder.AddColumn<DateTime>(
                name: "CompletedDate",
                table: "AssignmentSetProgressDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "AssignmentSetProgressDetails",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "AssignmentProgressDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssignmentSetProgressDetailId = table.Column<int>(type: "int", nullable: false),
                    AssignmentId = table.Column<int>(type: "int", nullable: false),
                    AnswerId = table.Column<int>(type: "int", nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentProgressDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssignmentProgressDetails_AssignmentSetProgressDetails_AssignmentSetProgressDetailId",
                        column: x => x.AssignmentSetProgressDetailId,
                        principalTable: "AssignmentSetProgressDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentProgressDetails_AssignmentSetProgressDetailId",
                table: "AssignmentProgressDetails",
                column: "AssignmentSetProgressDetailId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignmentProgressDetails");

            migrationBuilder.DropColumn(
                name: "CompletedDate",
                table: "AssignmentSetProgressDetails");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "AssignmentSetProgressDetails");

            migrationBuilder.RenameColumn(
                name: "Score",
                table: "AssignmentSetProgressDetails",
                newName: "AssignmentId");

            migrationBuilder.AddColumn<int>(
                name: "AnswerId",
                table: "AssignmentSetProgressDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
