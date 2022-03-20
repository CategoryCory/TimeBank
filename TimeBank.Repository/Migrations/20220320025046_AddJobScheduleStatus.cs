using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeBank.Repository.Migrations
{
    public partial class AddJobScheduleStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JobScheduleStatus",
                table: "JobSchedules",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "Open");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobScheduleStatus",
                table: "JobSchedules");
        }
    }
}
