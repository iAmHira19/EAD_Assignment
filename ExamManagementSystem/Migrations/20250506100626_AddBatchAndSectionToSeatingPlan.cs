using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddBatchAndSectionToSeatingPlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "SeatingPlans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Duration",
                table: "SeatingPlans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExamDate",
                table: "SeatingPlans",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "StartTime",
                table: "SeatingPlans",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.CreateIndex(
                name: "IX_SeatingPlans_CourseId",
                table: "SeatingPlans",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_SeatingPlans_Courses_CourseId",
                table: "SeatingPlans",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeatingPlans_Courses_CourseId",
                table: "SeatingPlans");

            migrationBuilder.DropIndex(
                name: "IX_SeatingPlans_CourseId",
                table: "SeatingPlans");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "SeatingPlans");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "SeatingPlans");

            migrationBuilder.DropColumn(
                name: "ExamDate",
                table: "SeatingPlans");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "SeatingPlans");
        }
    }
}
