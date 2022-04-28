using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mu3een.Migrations
{
    public partial class add_columns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VolunteerServices");

            migrationBuilder.AddColumn<bool>(
                name: "Turned",
                table: "VolunteerRewards",
                type: "bit",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Points",
                table: "SocialServices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "SocialServices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Rewards",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "VolunteerSocialServices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VolunteerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SocialServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Completed = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolunteerSocialServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VolunteerSocialServices_SocialServices_SocialServiceId",
                        column: x => x.SocialServiceId,
                        principalTable: "SocialServices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VolunteerSocialServices_Users_VolunteerId",
                        column: x => x.VolunteerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerSocialServices_SocialServiceId",
                table: "VolunteerSocialServices",
                column: "SocialServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerSocialServices_VolunteerId",
                table: "VolunteerSocialServices",
                column: "VolunteerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VolunteerSocialServices");

            migrationBuilder.DropColumn(
                name: "Turned",
                table: "VolunteerRewards");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "SocialServices");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "Rewards");

            migrationBuilder.AlterColumn<int>(
                name: "Points",
                table: "SocialServices",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "VolunteerServices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SocialServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VolunteerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolunteerServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VolunteerServices_SocialServices_SocialServiceId",
                        column: x => x.SocialServiceId,
                        principalTable: "SocialServices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VolunteerServices_Users_VolunteerId",
                        column: x => x.VolunteerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerServices_SocialServiceId",
                table: "VolunteerServices",
                column: "SocialServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerServices_VolunteerId",
                table: "VolunteerServices",
                column: "VolunteerId");
        }
    }
}
