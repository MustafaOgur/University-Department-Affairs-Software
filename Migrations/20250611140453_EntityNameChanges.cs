using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UDAS.Migrations
{
    /// <inheritdoc />
    public partial class EntityNameChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeatAssignment_SeatingPlan_SeatingPlanId",
                table: "SeatAssignment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SeatingPlan",
                table: "SeatingPlan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SeatAssignment",
                table: "SeatAssignment");

            migrationBuilder.RenameTable(
                name: "SeatingPlan",
                newName: "SeatingPlans");

            migrationBuilder.RenameTable(
                name: "SeatAssignment",
                newName: "SeatAssignments");

            migrationBuilder.RenameIndex(
                name: "IX_SeatAssignment_SeatingPlanId",
                table: "SeatAssignments",
                newName: "IX_SeatAssignments_SeatingPlanId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SeatingPlans",
                table: "SeatingPlans",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SeatAssignments",
                table: "SeatAssignments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SeatAssignments_SeatingPlans_SeatingPlanId",
                table: "SeatAssignments",
                column: "SeatingPlanId",
                principalTable: "SeatingPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeatAssignments_SeatingPlans_SeatingPlanId",
                table: "SeatAssignments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SeatingPlans",
                table: "SeatingPlans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SeatAssignments",
                table: "SeatAssignments");

            migrationBuilder.RenameTable(
                name: "SeatingPlans",
                newName: "SeatingPlan");

            migrationBuilder.RenameTable(
                name: "SeatAssignments",
                newName: "SeatAssignment");

            migrationBuilder.RenameIndex(
                name: "IX_SeatAssignments_SeatingPlanId",
                table: "SeatAssignment",
                newName: "IX_SeatAssignment_SeatingPlanId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SeatingPlan",
                table: "SeatingPlan",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SeatAssignment",
                table: "SeatAssignment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SeatAssignment_SeatingPlan_SeatingPlanId",
                table: "SeatAssignment",
                column: "SeatingPlanId",
                principalTable: "SeatingPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
