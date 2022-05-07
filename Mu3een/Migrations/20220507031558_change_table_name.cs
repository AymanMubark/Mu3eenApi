using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mu3een.Migrations
{
    public partial class change_table_name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VolunteerSocialEvents");

            migrationBuilder.CreateTable(
                name: "SocialEventVolunteers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VolunteerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SocialEventId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VolunteerStatus = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialEventVolunteers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SocialEventVolunteers_SocialEvents_SocialEventId",
                        column: x => x.SocialEventId,
                        principalTable: "SocialEvents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SocialEventVolunteers_Users_VolunteerId",
                        column: x => x.VolunteerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SocialEventVolunteers_SocialEventId",
                table: "SocialEventVolunteers",
                column: "SocialEventId");

            migrationBuilder.CreateIndex(
                name: "IX_SocialEventVolunteers_VolunteerId",
                table: "SocialEventVolunteers",
                column: "VolunteerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SocialEventVolunteers");

            migrationBuilder.CreateTable(
                name: "VolunteerSocialEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SocialEventId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VolunteerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VolunteerStatus = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolunteerSocialEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VolunteerSocialEvents_SocialEvents_SocialEventId",
                        column: x => x.SocialEventId,
                        principalTable: "SocialEvents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VolunteerSocialEvents_Users_VolunteerId",
                        column: x => x.VolunteerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerSocialEvents_SocialEventId",
                table: "VolunteerSocialEvents",
                column: "SocialEventId");

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerSocialEvents_VolunteerId",
                table: "VolunteerSocialEvents",
                column: "VolunteerId");
        }
    }
}
