using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameGallery.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserReviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameReviews",
                table: "GameReviews");

            migrationBuilder.AlterColumn<int>(
                name: "StarRating",
                table: "Reviews",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "PriceBefore",
                table: "Games",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Games",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "GameReviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameReviews",
                table: "GameReviews",
                columns: new[] { "GameId", "ReviewId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_GameReviews_UserId",
                table: "GameReviews",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameReviews_Users_UserId",
                table: "GameReviews",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameReviews_Users_UserId",
                table: "GameReviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameReviews",
                table: "GameReviews");

            migrationBuilder.DropIndex(
                name: "IX_GameReviews_UserId",
                table: "GameReviews");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "GameReviews");

            migrationBuilder.AlterColumn<decimal>(
                name: "StarRating",
                table: "Reviews",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "PriceBefore",
                table: "Games",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Games",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameReviews",
                table: "GameReviews",
                columns: new[] { "GameId", "ReviewId" });

            migrationBuilder.CreateTable(
                name: "UserReviews",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ReviewId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserReviews", x => new { x.UserId, x.ReviewId });
                    table.ForeignKey(
                        name: "FK_UserReviews_Reviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Reviews",
                        principalColumn: "ReviewId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserReviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserReviews_ReviewId",
                table: "UserReviews",
                column: "ReviewId");
        }
    }
}
