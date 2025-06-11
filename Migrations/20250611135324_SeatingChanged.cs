using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UDAS.Migrations
{
    /// <inheritdoc />
    public partial class SeatingChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Owner",
                table: "SeatingPlan");

            migrationBuilder.DropColumn(
                name: "SeatNumber",
                table: "SeatingPlan");

            migrationBuilder.AlterColumn<string>(
                name: "PlanName",
                table: "SeatingPlan",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "SeatAssignment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SeatNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    Owner = table.Column<string>(type: "TEXT", nullable: false),
                    SeatingPlanId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatAssignment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeatAssignment_SeatingPlan_SeatingPlanId",
                        column: x => x.SeatingPlanId,
                        principalTable: "SeatingPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SeatAssignment_SeatingPlanId",
                table: "SeatAssignment",
                column: "SeatingPlanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SeatAssignment");

            migrationBuilder.AlterColumn<string>(
                name: "PlanName",
                table: "SeatingPlan",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "Owner",
                table: "SeatingPlan",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeatNumber",
                table: "SeatingPlan",
                type: "TEXT",
                nullable: true);
        }
    }
}
