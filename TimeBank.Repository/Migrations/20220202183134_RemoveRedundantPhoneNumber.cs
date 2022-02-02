using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeBank.Repository.Migrations
{
    public partial class RemoveRedundantPhoneNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phone",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "AspNetUsers",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: true);
        }
    }
}
