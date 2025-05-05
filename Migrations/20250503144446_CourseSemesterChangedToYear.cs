using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UDAS.Migrations
{
    /// <inheritdoc />
    public partial class CourseSemesterChangedToYear : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Semester",
                table: "Courses",
                newName: "Year");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Year",
                table: "Courses",
                newName: "Semester");
        }
    }
}
