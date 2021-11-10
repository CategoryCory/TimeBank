using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeBank.Repository.Migrations
{
    public partial class AddGuidToJob : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Jobs",
                newName: "JobName");

            migrationBuilder.AddColumn<Guid>(
                name: "DisplayId",
                table: "Jobs",
                type: "uniqueidentifier",
                fixedLength: true,
                maxLength: 36,
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayId",
                table: "Jobs");

            migrationBuilder.RenameColumn(
                name: "JobName",
                table: "Jobs",
                newName: "Title");
        }
    }
}
