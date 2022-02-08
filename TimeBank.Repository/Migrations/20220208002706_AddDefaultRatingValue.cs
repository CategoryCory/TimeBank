using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeBank.Repository.Migrations
{
    public partial class AddDefaultRatingValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRating_AspNetUsers_AuthorId",
                table: "UserRating");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRating_AspNetUsers_RevieweeId",
                table: "UserRating");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRating",
                table: "UserRating");

            migrationBuilder.RenameTable(
                name: "UserRating",
                newName: "UserRatings");

            migrationBuilder.RenameIndex(
                name: "IX_UserRating_RevieweeId",
                table: "UserRatings",
                newName: "IX_UserRatings_RevieweeId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRating_AuthorId",
                table: "UserRatings",
                newName: "IX_UserRatings_AuthorId");

            migrationBuilder.AlterColumn<double>(
                name: "Rating",
                table: "UserRatings",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRatings",
                table: "UserRatings",
                column: "UserRatingId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRatings_AspNetUsers_AuthorId",
                table: "UserRatings",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRatings_AspNetUsers_RevieweeId",
                table: "UserRatings",
                column: "RevieweeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRatings_AspNetUsers_AuthorId",
                table: "UserRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRatings_AspNetUsers_RevieweeId",
                table: "UserRatings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRatings",
                table: "UserRatings");

            migrationBuilder.RenameTable(
                name: "UserRatings",
                newName: "UserRating");

            migrationBuilder.RenameIndex(
                name: "IX_UserRatings_RevieweeId",
                table: "UserRating",
                newName: "IX_UserRating_RevieweeId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRatings_AuthorId",
                table: "UserRating",
                newName: "IX_UserRating_AuthorId");

            migrationBuilder.AlterColumn<double>(
                name: "Rating",
                table: "UserRating",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float",
                oldDefaultValue: 0.0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRating",
                table: "UserRating",
                column: "UserRatingId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRating_AspNetUsers_AuthorId",
                table: "UserRating",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRating_AspNetUsers_RevieweeId",
                table: "UserRating",
                column: "RevieweeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
