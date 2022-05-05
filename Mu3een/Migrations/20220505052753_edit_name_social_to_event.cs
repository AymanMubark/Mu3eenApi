using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mu3een.Migrations
{
    public partial class edit_name_social_to_event : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VolunteerSocialServices");

            migrationBuilder.DropTable(
                name: "SocialServices");

            migrationBuilder.DropTable(
                name: "SocialServiceTypes");

            migrationBuilder.CreateTable(
                name: "SocialEventTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialEventTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SocialEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstitutionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SocialEventTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RegionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VolunteerRequried = table.Column<int>(type: "int", nullable: true),
                    Points = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SocialEvents_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SocialEvents_SocialEventTypes_SocialEventTypeId",
                        column: x => x.SocialEventTypeId,
                        principalTable: "SocialEventTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SocialEvents_Users_InstitutionId",
                        column: x => x.InstitutionId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VolunteerSocialEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VolunteerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SocialEventId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Completed = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
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
                name: "IX_SocialEvents_InstitutionId",
                table: "SocialEvents",
                column: "InstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_SocialEvents_RegionId",
                table: "SocialEvents",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_SocialEvents_SocialEventTypeId",
                table: "SocialEvents",
                column: "SocialEventTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerSocialEvents_SocialEventId",
                table: "VolunteerSocialEvents",
                column: "SocialEventId");

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerSocialEvents_VolunteerId",
                table: "VolunteerSocialEvents",
                column: "VolunteerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VolunteerSocialEvents");

            migrationBuilder.DropTable(
                name: "SocialEvents");

            migrationBuilder.DropTable(
                name: "SocialEventTypes");

            migrationBuilder.CreateTable(
                name: "SocialServiceTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialServiceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SocialServices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InstitutionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RegionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SocialServiceTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Points = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VolunteerRequried = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SocialServices_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SocialServices_SocialServiceTypes_SocialServiceTypeId",
                        column: x => x.SocialServiceTypeId,
                        principalTable: "SocialServiceTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SocialServices_Users_InstitutionId",
                        column: x => x.InstitutionId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VolunteerSocialServices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SocialServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    VolunteerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Completed = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                name: "IX_SocialServices_InstitutionId",
                table: "SocialServices",
                column: "InstitutionId");

            migrationBuilder.CreateIndex(
                name: "IX_SocialServices_RegionId",
                table: "SocialServices",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_SocialServices_SocialServiceTypeId",
                table: "SocialServices",
                column: "SocialServiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerSocialServices_SocialServiceId",
                table: "VolunteerSocialServices",
                column: "SocialServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerSocialServices_VolunteerId",
                table: "VolunteerSocialServices",
                column: "VolunteerId");
        }
    }
}
