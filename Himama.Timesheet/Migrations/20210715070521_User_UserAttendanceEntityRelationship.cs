using Microsoft.EntityFrameworkCore.Migrations;

namespace Himama.Timesheet.Migrations
{
    public partial class User_UserAttendanceEntityRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UserAttendance",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserAttendance_UserId",
                table: "UserAttendance",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAttendance_User_UserId",
                table: "UserAttendance",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAttendance_User_UserId",
                table: "UserAttendance");

            migrationBuilder.DropIndex(
                name: "IX_UserAttendance_UserId",
                table: "UserAttendance");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserAttendance");
        }
    }
}
