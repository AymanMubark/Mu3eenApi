using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mu3een.Migrations
{
    public partial class add_NameAr_tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NameAr",
                table: "SocialEventTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameAr",
                table: "Regions",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameAr",
                table: "SocialEventTypes");

            migrationBuilder.DropColumn(
                name: "NameAr",
                table: "Regions");
        }
    }
}
