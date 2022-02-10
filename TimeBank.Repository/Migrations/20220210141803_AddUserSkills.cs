using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeBank.Repository.Migrations
{
    public partial class AddUserSkills : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserSkills",
                columns: table => new
                {
                    UserSkillId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkillName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SkillNameSlug = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSkills", x => x.UserSkillId);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserUserSkill",
                columns: table => new
                {
                    SkillsUserSkillId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserUserSkill", x => new { x.SkillsUserSkillId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserUserSkill_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserUserSkill_UserSkills_SkillsUserSkillId",
                        column: x => x.SkillsUserSkillId,
                        principalTable: "UserSkills",
                        principalColumn: "UserSkillId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserUserSkill_UsersId",
                table: "ApplicationUserUserSkill",
                column: "UsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserUserSkill");

            migrationBuilder.DropTable(
                name: "UserSkills");
        }
    }
}
