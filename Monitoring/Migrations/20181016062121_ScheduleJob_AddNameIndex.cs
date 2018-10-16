using Microsoft.EntityFrameworkCore.Migrations;

namespace Monitoring.Migrations
{
    public partial class ScheduleJob_AddNameIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ScheduleJobs_Name",
                table: "ScheduleJobs",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ScheduleJobs_Name",
                table: "ScheduleJobs");
        }
    }
}
