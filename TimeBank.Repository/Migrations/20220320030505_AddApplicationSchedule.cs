using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeBank.Repository.Migrations
{
    public partial class AddApplicationSchedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationSchedule",
                columns: table => new
                {
                    JobApplicationsJobApplicationId = table.Column<int>(type: "int", nullable: false),
                    JobSchedulesJobScheduleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationSchedule", x => new { x.JobApplicationsJobApplicationId, x.JobSchedulesJobScheduleId });
                    table.ForeignKey(
                        name: "FK_ApplicationSchedule_JobApplications_JobApplicationsJobApplicationId",
                        column: x => x.JobApplicationsJobApplicationId,
                        principalTable: "JobApplications",
                        principalColumn: "JobApplicationId");
                    table.ForeignKey(
                        name: "FK_ApplicationSchedule_JobSchedules_JobSchedulesJobScheduleId",
                        column: x => x.JobSchedulesJobScheduleId,
                        principalTable: "JobSchedules",
                        principalColumn: "JobScheduleId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationSchedule_JobSchedulesJobScheduleId",
                table: "ApplicationSchedule",
                column: "JobSchedulesJobScheduleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationSchedule");
        }
    }
}
