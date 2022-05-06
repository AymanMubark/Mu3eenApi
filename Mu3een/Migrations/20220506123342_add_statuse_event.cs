using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mu3een.Migrations
{
    public partial class add_statuse_event : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Completed",
                table: "VolunteerSocialEvents");

            migrationBuilder.AddColumn<int>(
                name: "VolunteerStatus",
                table: "VolunteerSocialEvents",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiryDate",
                table: "Rewards",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VolunteerStatus",
                table: "VolunteerSocialEvents");

            migrationBuilder.DropColumn(
                name: "ExpiryDate",
                table: "Rewards");

            migrationBuilder.AddColumn<bool>(
                name: "Completed",
                table: "VolunteerSocialEvents",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
