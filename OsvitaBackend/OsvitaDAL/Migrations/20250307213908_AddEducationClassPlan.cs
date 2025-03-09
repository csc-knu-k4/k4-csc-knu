using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OsvitaDAL.Migrations
{
    /// <inheritdoc />
    public partial class AddEducationClassPlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EducationClassPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EducationClassId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationClassPlans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EducationClassPlans_EducationClasses_EducationClassId",
                        column: x => x.EducationClassId,
                        principalTable: "EducationClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssignmentSetPlanDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EducationClassPlanId = table.Column<int>(type: "int", nullable: true),
                    AssignmentSetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentSetPlanDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssignmentSetPlanDetails_EducationClassPlans_EducationClassPlanId",
                        column: x => x.EducationClassPlanId,
                        principalTable: "EducationClassPlans",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentSetPlanDetails_EducationClassPlanId",
                table: "AssignmentSetPlanDetails",
                column: "EducationClassPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationClassPlans_EducationClassId",
                table: "EducationClassPlans",
                column: "EducationClassId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignmentSetPlanDetails");

            migrationBuilder.DropTable(
                name: "EducationClassPlans");
        }
    }
}
