using Microsoft.EntityFrameworkCore.Migrations;

namespace Tinderro.API.Migrations
{
    public partial class AddedLikes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    UserLikesId = table.Column<int>(nullable: false),
                    SomeoneLikesMeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Likes", x => new { x.UserLikesId, x.SomeoneLikesMeId });
                    table.ForeignKey(
                        name: "FK_Likes_users_SomeoneLikesMeId",
                        column: x => x.SomeoneLikesMeId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Likes_users_UserLikesId",
                        column: x => x.UserLikesId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Likes_SomeoneLikesMeId",
                table: "Likes",
                column: "SomeoneLikesMeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Likes");
        }
    }
}
