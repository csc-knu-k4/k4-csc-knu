using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OsvitaDAL.Migrations
{
    /// <inheritdoc />
    public partial class AddAssigmentSetPlanDetailToEducationPlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EducationPlanId",
                table: "AssignmentSetPlanDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentSetPlanDetails_EducationPlanId",
                table: "AssignmentSetPlanDetails",
                column: "EducationPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentSetPlanDetails_EducationPlans_EducationPlanId",
                table: "AssignmentSetPlanDetails",
                column: "EducationPlanId",
                principalTable: "EducationPlans",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentSetPlanDetails_EducationPlans_EducationPlanId",
                table: "AssignmentSetPlanDetails");

            migrationBuilder.DropIndex(
                name: "IX_AssignmentSetPlanDetails_EducationPlanId",
                table: "AssignmentSetPlanDetails");

            migrationBuilder.DropColumn(
                name: "EducationPlanId",
                table: "AssignmentSetPlanDetails");
        }
    }
}
