using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mu3een.Migrations
{
    public partial class remove_change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Turned",
                table: "VolunteerRewards");

            migrationBuilder.AlterColumn<int>(
                name: "Points",
                table: "Rewards",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Turned",
                table: "VolunteerRewards",
                type: "bit",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Points",
                table: "Rewards",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
