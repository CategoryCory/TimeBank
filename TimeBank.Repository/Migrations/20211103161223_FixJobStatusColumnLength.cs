using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeBank.Repository.Migrations
{
    public partial class FixJobStatusColumnLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "JobStatus",
                table: "Jobs",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "Available",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "Available");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "JobStatus",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Available",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldDefaultValue: "Available");
        }
    }
}
