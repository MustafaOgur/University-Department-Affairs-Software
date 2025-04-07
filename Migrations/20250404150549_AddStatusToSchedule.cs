using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UDAS.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusToSchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "scheduleNumber",
                table: "Schedules");

            migrationBuilder.AddColumn<string>(
                name: "scheduleName",
                table: "Schedules",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "status",
                table: "Schedules",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_scheduleName",
                table: "Schedules",
                column: "scheduleName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Schedules_scheduleName",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "scheduleName",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "status",
                table: "Schedules");

            migrationBuilder.AddColumn<int>(
                name: "scheduleNumber",
                table: "Schedules",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
