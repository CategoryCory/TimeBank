using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeBank.Repository.Migrations
{
    public partial class AddMsgThreadUniqueIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MessageThreads_JobId",
                table: "MessageThreads");

            migrationBuilder.CreateIndex(
                name: "IX_MessageThreads_JobId_ToUserId_FromUserId",
                table: "MessageThreads",
                columns: new[] { "JobId", "ToUserId", "FromUserId" },
                unique: true,
                filter: "[ToUserId] IS NOT NULL AND [FromUserId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MessageThreads_JobId_ToUserId_FromUserId",
                table: "MessageThreads");

            migrationBuilder.CreateIndex(
                name: "IX_MessageThreads_JobId",
                table: "MessageThreads",
                column: "JobId");
        }
    }
}
