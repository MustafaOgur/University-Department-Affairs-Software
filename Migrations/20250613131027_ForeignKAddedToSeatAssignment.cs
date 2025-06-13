using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UDAS.Migrations
{
    /// <inheritdoc />
    public partial class ForeignKAddedToSeatAssignment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClassroomId",
                table: "SeatAssignments",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SeatAssignments_ClassroomId",
                table: "SeatAssignments",
                column: "ClassroomId");

            migrationBuilder.AddForeignKey(
                name: "FK_SeatAssignments_Classrooms_ClassroomId",
                table: "SeatAssignments",
                column: "ClassroomId",
                principalTable: "Classrooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeatAssignments_Classrooms_ClassroomId",
                table: "SeatAssignments");

            migrationBuilder.DropIndex(
                name: "IX_SeatAssignments_ClassroomId",
                table: "SeatAssignments");

            migrationBuilder.DropColumn(
                name: "ClassroomId",
                table: "SeatAssignments");
        }
    }
}
