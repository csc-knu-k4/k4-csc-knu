using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OsvitaDAL.Migrations
{
    /// <inheritdoc />
    public partial class AddAssignments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssignmentLinks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssignmentId = table.Column<int>(type: "int", nullable: false),
                    ObjectId = table.Column<int>(type: "int", nullable: false),
                    ObjectType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentLinks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Assignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProblemDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Explanation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssignmentType = table.Column<int>(type: "int", nullable: false),
                    ParentAssignmentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assignments_Assignments_ParentAssignmentId",
                        column: x => x.ParentAssignmentId,
                        principalTable: "Assignments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false),
                    AssignmentId = table.Column<int>(type: "int", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_Assignments_AssignmentId",
                        column: x => x.AssignmentId,
                        principalTable: "Assignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_AssignmentId",
                table: "Answers",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_ParentAssignmentId",
                table: "Assignments",
                column: "ParentAssignmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "AssignmentLinks");

            migrationBuilder.DropTable(
                name: "Assignments");
        }
    }
}
