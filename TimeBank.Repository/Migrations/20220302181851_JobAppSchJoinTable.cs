using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeBank.Repository.Migrations
{
    public partial class JobAppSchJoinTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobApplicationSchedules",
                columns: table => new
                {
                    JobScheduleId = table.Column<int>(type: "int", nullable: false),
                    JobApplicationId = table.Column<int>(type: "int", nullable: false),
                    JobApplicationScheduleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobApplicationSchedules", x => new { x.JobScheduleId, x.JobApplicationId });
                    table.ForeignKey(
                        name: "FK_JobApplicationSchedules_JobApplications_JobApplicationId",
                        column: x => x.JobApplicationId,
                        principalTable: "JobApplications",
                        principalColumn: "JobApplicationId");
                    table.ForeignKey(
                        name: "FK_JobApplicationSchedules_JobSchedules_JobScheduleId",
                        column: x => x.JobScheduleId,
                        principalTable: "JobSchedules",
                        principalColumn: "JobScheduleId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobApplicationSchedules_JobApplicationId",
                table: "JobApplicationSchedules",
                column: "JobApplicationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobApplicationSchedules");
        }
    }
}
