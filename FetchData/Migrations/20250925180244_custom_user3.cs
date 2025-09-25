using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FetchData.Migrations
{
    /// <inheritdoc />
    public partial class custom_user3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCourses_Courses_CourseId1",
                table: "UserCourses");

            migrationBuilder.DropIndex(
                name: "IX_UserCourses_CourseId1",
                table: "UserCourses");

            migrationBuilder.DropColumn(
                name: "CourseId1",
                table: "UserCourses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseId1",
                table: "UserCourses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserCourses_CourseId1",
                table: "UserCourses",
                column: "CourseId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCourses_Courses_CourseId1",
                table: "UserCourses",
                column: "CourseId1",
                principalTable: "Courses",
                principalColumn: "Id");
        }
    }
}
