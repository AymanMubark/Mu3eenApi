using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mu3een.Migrations
{
    public partial class change_provider_institution : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rewards_Users_ProviderId",
                table: "Rewards");

            migrationBuilder.DropForeignKey(
                name: "FK_SocialServices_Users_ProviderId",
                table: "SocialServices");

            migrationBuilder.RenameColumn(
                name: "ProviderId",
                table: "SocialServices",
                newName: "InstitutionId");

            migrationBuilder.RenameIndex(
                name: "IX_SocialServices_ProviderId",
                table: "SocialServices",
                newName: "IX_SocialServices_InstitutionId");

            migrationBuilder.RenameColumn(
                name: "ProviderId",
                table: "Rewards",
                newName: "InstitutionId");

            migrationBuilder.RenameIndex(
                name: "IX_Rewards_ProviderId",
                table: "Rewards",
                newName: "IX_Rewards_InstitutionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rewards_Users_InstitutionId",
                table: "Rewards",
                column: "InstitutionId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SocialServices_Users_InstitutionId",
                table: "SocialServices",
                column: "InstitutionId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rewards_Users_InstitutionId",
                table: "Rewards");

            migrationBuilder.DropForeignKey(
                name: "FK_SocialServices_Users_InstitutionId",
                table: "SocialServices");

            migrationBuilder.RenameColumn(
                name: "InstitutionId",
                table: "SocialServices",
                newName: "ProviderId");

            migrationBuilder.RenameIndex(
                name: "IX_SocialServices_InstitutionId",
                table: "SocialServices",
                newName: "IX_SocialServices_ProviderId");

            migrationBuilder.RenameColumn(
                name: "InstitutionId",
                table: "Rewards",
                newName: "ProviderId");

            migrationBuilder.RenameIndex(
                name: "IX_Rewards_InstitutionId",
                table: "Rewards",
                newName: "IX_Rewards_ProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rewards_Users_ProviderId",
                table: "Rewards",
                column: "ProviderId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SocialServices_Users_ProviderId",
                table: "SocialServices",
                column: "ProviderId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
