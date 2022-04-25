using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mu3een.Migrations
{
    public partial class _InitalCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Regions",
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
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SocialServiceTypes",
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
                    table.PrimaryKey("PK_SocialServiceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Admin_UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Admin_Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Volunteer_Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OTP = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    Points = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rewards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Points = table.Column<int>(type: "int", nullable: true),
                    Numbers = table.Column<int>(type: "int", nullable: true),
                    ProviderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rewards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rewards_Users_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SocialServices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SocialServiceTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RegionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VolunteerRequried = table.Column<int>(type: "int", nullable: true),
                    Points = table.Column<int>(type: "int", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: true),
                    Longitude = table.Column<double>(type: "float", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
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
                        name: "FK_SocialServices_Users_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VolunteerRewards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VolunteerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RewardId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolunteerRewards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VolunteerRewards_Rewards_RewardId",
                        column: x => x.RewardId,
                        principalTable: "Rewards",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VolunteerRewards_Users_VolunteerId",
                        column: x => x.VolunteerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VolunteerServices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VolunteerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SocialServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
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
                name: "IX_Rewards_ProviderId",
                table: "Rewards",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_SocialServices_ProviderId",
                table: "SocialServices",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_SocialServices_RegionId",
                table: "SocialServices",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_SocialServices_SocialServiceTypeId",
                table: "SocialServices",
                column: "SocialServiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerRewards_RewardId",
                table: "VolunteerRewards",
                column: "RewardId");

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerRewards_VolunteerId",
                table: "VolunteerRewards",
                column: "VolunteerId");

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerServices_SocialServiceId",
                table: "VolunteerServices",
                column: "SocialServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerServices_VolunteerId",
                table: "VolunteerServices",
                column: "VolunteerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VolunteerRewards");

            migrationBuilder.DropTable(
                name: "VolunteerServices");

            migrationBuilder.DropTable(
                name: "Rewards");

            migrationBuilder.DropTable(
                name: "SocialServices");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "SocialServiceTypes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
