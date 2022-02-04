using Microsoft.EntityFrameworkCore.Migrations;

namespace BankApp.Services.Migrations
{
    public partial class v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_UserAccounts_ReceiverId",
                schema: "practice",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_UserAccounts_SenderId",
                schema: "practice",
                table: "Transactions");

            migrationBuilder.AlterColumn<string>(
                name: "SenderId",
                schema: "practice",
                table: "Transactions",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25,
                oldDefaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ReceiverId",
                schema: "practice",
                table: "Transactions",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25,
                oldDefaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_UserAccounts_ReceiverId",
                schema: "practice",
                table: "Transactions",
                column: "ReceiverId",
                principalSchema: "practice",
                principalTable: "UserAccounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_UserAccounts_SenderId",
                schema: "practice",
                table: "Transactions",
                column: "SenderId",
                principalSchema: "practice",
                principalTable: "UserAccounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_UserAccounts_ReceiverId",
                schema: "practice",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_UserAccounts_SenderId",
                schema: "practice",
                table: "Transactions");

            migrationBuilder.AlterColumn<string>(
                name: "SenderId",
                schema: "practice",
                table: "Transactions",
                type: "varchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReceiverId",
                schema: "practice",
                table: "Transactions",
                type: "varchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_UserAccounts_ReceiverId",
                schema: "practice",
                table: "Transactions",
                column: "ReceiverId",
                principalSchema: "practice",
                principalTable: "UserAccounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_UserAccounts_SenderId",
                schema: "practice",
                table: "Transactions",
                column: "SenderId",
                principalSchema: "practice",
                principalTable: "UserAccounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
