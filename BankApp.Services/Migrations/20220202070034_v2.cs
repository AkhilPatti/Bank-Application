using Microsoft.EntityFrameworkCore.Migrations;

namespace BankApp.Services.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StaffAccounts_Banks_StaffId",
                schema: "practice",
                table: "StaffAccounts");

            migrationBuilder.CreateIndex(
                name: "IX_StaffAccounts_BankId",
                schema: "practice",
                table: "StaffAccounts",
                column: "BankId");

            migrationBuilder.AddForeignKey(
                name: "FK_StaffAccounts_Banks_BankId",
                schema: "practice",
                table: "StaffAccounts",
                column: "BankId",
                principalSchema: "practice",
                principalTable: "Banks",
                principalColumn: "BankId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StaffAccounts_Banks_BankId",
                schema: "practice",
                table: "StaffAccounts");

            migrationBuilder.DropIndex(
                name: "IX_StaffAccounts_BankId",
                schema: "practice",
                table: "StaffAccounts");

            migrationBuilder.AddForeignKey(
                name: "FK_StaffAccounts_Banks_StaffId",
                schema: "practice",
                table: "StaffAccounts",
                column: "StaffId",
                principalSchema: "practice",
                principalTable: "Banks",
                principalColumn: "BankId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
