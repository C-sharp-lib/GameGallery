using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameGallery.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabase01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserComments_Comments_CommentsCommentId",
                table: "UserComments");

            migrationBuilder.DropForeignKey(
                name: "FK_UserComments_Users_UsersUserId",
                table: "UserComments");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Comments_CommentsCommentId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CommentsCommentId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_UserComments_CommentsCommentId",
                table: "UserComments");

            migrationBuilder.DropIndex(
                name: "IX_UserComments_UsersUserId",
                table: "UserComments");

            migrationBuilder.DropColumn(
                name: "CommentsCommentId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CommentsCommentId",
                table: "UserComments");

            migrationBuilder.DropColumn(
                name: "UsersUserId",
                table: "UserComments");

            migrationBuilder.CreateIndex(
                name: "IX_UserComments_CommentId",
                table: "UserComments",
                column: "CommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserComments_Comments_CommentId",
                table: "UserComments",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "CommentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserComments_Users_UserId",
                table: "UserComments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserComments_Comments_CommentId",
                table: "UserComments");

            migrationBuilder.DropForeignKey(
                name: "FK_UserComments_Users_UserId",
                table: "UserComments");

            migrationBuilder.DropIndex(
                name: "IX_UserComments_CommentId",
                table: "UserComments");

            migrationBuilder.AddColumn<int>(
                name: "CommentsCommentId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CommentsCommentId",
                table: "UserComments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsersUserId",
                table: "UserComments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CommentsCommentId",
                table: "Users",
                column: "CommentsCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserComments_CommentsCommentId",
                table: "UserComments",
                column: "CommentsCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserComments_UsersUserId",
                table: "UserComments",
                column: "UsersUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserComments_Comments_CommentsCommentId",
                table: "UserComments",
                column: "CommentsCommentId",
                principalTable: "Comments",
                principalColumn: "CommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserComments_Users_UsersUserId",
                table: "UserComments",
                column: "UsersUserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Comments_CommentsCommentId",
                table: "Users",
                column: "CommentsCommentId",
                principalTable: "Comments",
                principalColumn: "CommentId");
        }
    }
}
