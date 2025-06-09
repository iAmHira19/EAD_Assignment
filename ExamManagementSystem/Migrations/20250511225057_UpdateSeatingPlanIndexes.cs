using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeatingPlanIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeatingPlans_Courses_CourseId",
                table: "SeatingPlans");

            migrationBuilder.DropIndex(
                name: "IX_SeatingPlans_RoomId",
                table: "SeatingPlans");

            migrationBuilder.DropIndex(
                name: "IX_SeatingPlans_StudentId",
                table: "SeatingPlans");

            migrationBuilder.CreateIndex(
                name: "IX_SeatingPlans_RoomConflict",
                table: "SeatingPlans",
                columns: new[] { "RoomId", "ExamDate", "StartTime" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SeatingPlans_StudentCourseExam",
                table: "SeatingPlans",
                columns: new[] { "StudentId", "CourseId", "ExamDate", "StartTime" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SeatingPlans_Courses_CourseId",
                table: "SeatingPlans",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeatingPlans_Courses_CourseId",
                table: "SeatingPlans");

            migrationBuilder.DropIndex(
                name: "IX_SeatingPlans_RoomConflict",
                table: "SeatingPlans");

            migrationBuilder.DropIndex(
                name: "IX_SeatingPlans_StudentCourseExam",
                table: "SeatingPlans");

            migrationBuilder.CreateIndex(
                name: "IX_SeatingPlans_RoomId",
                table: "SeatingPlans",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_SeatingPlans_StudentId",
                table: "SeatingPlans",
                column: "StudentId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SeatingPlans_Courses_CourseId",
                table: "SeatingPlans",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
