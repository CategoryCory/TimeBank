using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeBank.Repository.Migrations
{
    public partial class AddJobCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "85e8872d-ef53-4d30-ae27-38f44733cd7f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a827432a-d157-4f69-a275-5848946c9c67");

            migrationBuilder.AddColumn<int>(
                name: "JobCategoryId",
                table: "Jobs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "JobCategories",
                columns: table => new
                {
                    JobCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobCategoryName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    JobCategorySlug = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobCategories", x => x.JobCategoryId);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3f1ba4bc-5e9b-414a-97f2-3d6cc193e10c", "6880f1a1-ceff-44e1-be05-d1625a54c31a", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "dd0b8920-1c72-4bfa-8ea1-b6736576a7b3", "a71b5d1e-a3d0-444f-ab92-10355a29ecf1", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_JobCategoryId",
                table: "Jobs",
                column: "JobCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_JobCategories_JobCategoryId",
                table: "Jobs",
                column: "JobCategoryId",
                principalTable: "JobCategories",
                principalColumn: "JobCategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_JobCategories_JobCategoryId",
                table: "Jobs");

            migrationBuilder.DropTable(
                name: "JobCategories");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_JobCategoryId",
                table: "Jobs");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3f1ba4bc-5e9b-414a-97f2-3d6cc193e10c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dd0b8920-1c72-4bfa-8ea1-b6736576a7b3");

            migrationBuilder.DropColumn(
                name: "JobCategoryId",
                table: "Jobs");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "85e8872d-ef53-4d30-ae27-38f44733cd7f", "0ad34e75-e22d-44c9-8584-5efc7e1a5574", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a827432a-d157-4f69-a275-5848946c9c67", "7adb7309-bc42-4f8f-94e5-c068faa53ede", "User", "USER" });
        }
    }
}
