using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UDAS.Migrations
{
    /// <inheritdoc />
    public partial class FKAddedToExamSchedules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SeatingPlanId",
                table: "ExamSchedules",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExamSchedules_SeatingPlanId",
                table: "ExamSchedules",
                column: "SeatingPlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamSchedules_SeatingPlans_SeatingPlanId",
                table: "ExamSchedules",
                column: "SeatingPlanId",
                principalTable: "SeatingPlans",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamSchedules_SeatingPlans_SeatingPlanId",
                table: "ExamSchedules");

            migrationBuilder.DropIndex(
                name: "IX_ExamSchedules_SeatingPlanId",
                table: "ExamSchedules");

            migrationBuilder.DropColumn(
                name: "SeatingPlanId",
                table: "ExamSchedules");
        }
    }
}
