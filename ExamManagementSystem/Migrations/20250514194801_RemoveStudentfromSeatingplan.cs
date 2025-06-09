using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class RemoveStudentfromSeatingplan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeatingPlans_Students_StudentId",
                table: "SeatingPlans");

            migrationBuilder.DropIndex(
                name: "IX_SeatingPlans_StudentCourseExam",
                table: "SeatingPlans");

            migrationBuilder.CreateIndex(
                name: "IX_SeatingPlans_StudentId",
                table: "SeatingPlans",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_SeatingPlans_Students_StudentId",
                table: "SeatingPlans",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeatingPlans_Students_StudentId",
                table: "SeatingPlans");

            migrationBuilder.DropIndex(
                name: "IX_SeatingPlans_StudentId",
                table: "SeatingPlans");

            migrationBuilder.CreateIndex(
                name: "IX_SeatingPlans_StudentCourseExam",
                table: "SeatingPlans",
                columns: new[] { "StudentId", "CourseId", "ExamDate", "StartTime" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SeatingPlans_Students_StudentId",
                table: "SeatingPlans",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
