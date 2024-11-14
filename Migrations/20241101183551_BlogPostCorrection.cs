using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameGallery.Migrations
{
    /// <inheritdoc />
    public partial class BlogPostCorrection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_Users_UserBlogId",
                table: "BlogPosts");

            migrationBuilder.DropIndex(
                name: "IX_BlogPosts_UserBlogId",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "UserBlogId",
                table: "BlogPosts");

            migrationBuilder.AddColumn<string>(
                name: "ImageOne",
                table: "BlogPosts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageThree",
                table: "BlogPosts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageTwo",
                table: "BlogPosts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageOne",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "ImageThree",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "ImageTwo",
                table: "BlogPosts");

            migrationBuilder.AddColumn<int>(
                name: "UserBlogId",
                table: "BlogPosts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_UserBlogId",
                table: "BlogPosts",
                column: "UserBlogId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_Users_UserBlogId",
                table: "BlogPosts",
                column: "UserBlogId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
