using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeatingPlanRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeatingPlans_Students_StudentId",
                table: "SeatingPlans");

            migrationBuilder.DropIndex(
                name: "IX_SeatingPlans_StudentId",
                table: "SeatingPlans");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "SeatingPlans");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "SeatingPlans",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
    }
}
