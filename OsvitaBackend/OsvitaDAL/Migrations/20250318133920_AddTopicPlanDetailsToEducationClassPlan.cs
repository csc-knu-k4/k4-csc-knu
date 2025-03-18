using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OsvitaDAL.Migrations
{
    /// <inheritdoc />
    public partial class AddTopicPlanDetailsToEducationClassPlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TopicPlanDetails_EducationPlans_EducationPlanId",
                table: "TopicPlanDetails");

            migrationBuilder.AlterColumn<int>(
                name: "EducationPlanId",
                table: "TopicPlanDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "EducationClassPlanId",
                table: "TopicPlanDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TopicPlanDetails_EducationClassPlanId",
                table: "TopicPlanDetails",
                column: "EducationClassPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_TopicPlanDetails_EducationClassPlans_EducationClassPlanId",
                table: "TopicPlanDetails",
                column: "EducationClassPlanId",
                principalTable: "EducationClassPlans",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TopicPlanDetails_EducationPlans_EducationPlanId",
                table: "TopicPlanDetails",
                column: "EducationPlanId",
                principalTable: "EducationPlans",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TopicPlanDetails_EducationClassPlans_EducationClassPlanId",
                table: "TopicPlanDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_TopicPlanDetails_EducationPlans_EducationPlanId",
                table: "TopicPlanDetails");

            migrationBuilder.DropIndex(
                name: "IX_TopicPlanDetails_EducationClassPlanId",
                table: "TopicPlanDetails");

            migrationBuilder.DropColumn(
                name: "EducationClassPlanId",
                table: "TopicPlanDetails");

            migrationBuilder.AlterColumn<int>(
                name: "EducationPlanId",
                table: "TopicPlanDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TopicPlanDetails_EducationPlans_EducationPlanId",
                table: "TopicPlanDetails",
                column: "EducationPlanId",
                principalTable: "EducationPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
