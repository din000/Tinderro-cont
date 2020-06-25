using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tinderro.API.Migrations
{
    public partial class ExtendedUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Children",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "users",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "users",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Education",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EyeColor",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FreeTime",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FriendeWouldDescribeMe",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Growth",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HairColor",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ILike",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdoNotLike",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Interests",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ItFeelsBestIn",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Languages",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastActive",
                table: "users",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LookingFor",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MakesMeLaugh",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MartialStatus",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Motto",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Movies",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Music",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Personality",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Profession",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sport",
                table: "users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ZodiacSign",
                table: "users",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "photos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    IsMain = table.Column<bool>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_photos_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_photos_UserId",
                table: "photos",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "photos");

            migrationBuilder.DropColumn(
                name: "Children",
                table: "users");

            migrationBuilder.DropColumn(
                name: "City",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "users");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Education",
                table: "users");

            migrationBuilder.DropColumn(
                name: "EyeColor",
                table: "users");

            migrationBuilder.DropColumn(
                name: "FreeTime",
                table: "users");

            migrationBuilder.DropColumn(
                name: "FriendeWouldDescribeMe",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Growth",
                table: "users");

            migrationBuilder.DropColumn(
                name: "HairColor",
                table: "users");

            migrationBuilder.DropColumn(
                name: "ILike",
                table: "users");

            migrationBuilder.DropColumn(
                name: "IdoNotLike",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Interests",
                table: "users");

            migrationBuilder.DropColumn(
                name: "ItFeelsBestIn",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Languages",
                table: "users");

            migrationBuilder.DropColumn(
                name: "LastActive",
                table: "users");

            migrationBuilder.DropColumn(
                name: "LookingFor",
                table: "users");

            migrationBuilder.DropColumn(
                name: "MakesMeLaugh",
                table: "users");

            migrationBuilder.DropColumn(
                name: "MartialStatus",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Motto",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Movies",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Music",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Personality",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Profession",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Sport",
                table: "users");

            migrationBuilder.DropColumn(
                name: "ZodiacSign",
                table: "users");
        }
    }
}
