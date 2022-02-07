using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeBank.Repository.Migrations
{
    public partial class ChangeJobApplicationForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_Jobs_JobId",
                table: "JobApplications");

            migrationBuilder.DropIndex(
                name: "IX_JobApplications_JobId",
                table: "JobApplications");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "JobApplications");

            migrationBuilder.AlterColumn<Guid>(
                name: "JobDisplayId",
                table: "JobApplications",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Jobs_DisplayId",
                table: "Jobs",
                column: "DisplayId");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_JobDisplayId",
                table: "JobApplications",
                column: "JobDisplayId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_Jobs_JobDisplayId",
                table: "JobApplications",
                column: "JobDisplayId",
                principalTable: "Jobs",
                principalColumn: "DisplayId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_Jobs_JobDisplayId",
                table: "JobApplications");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Jobs_DisplayId",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_JobApplications_JobDisplayId",
                table: "JobApplications");

            migrationBuilder.AlterColumn<string>(
                name: "JobDisplayId",
                table: "JobApplications",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<int>(
                name: "JobId",
                table: "JobApplications",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_JobId",
                table: "JobApplications",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_Jobs_JobId",
                table: "JobApplications",
                column: "JobId",
                principalTable: "Jobs",
                principalColumn: "JobId");
        }
    }
}
