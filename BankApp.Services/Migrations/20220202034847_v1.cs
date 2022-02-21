using Microsoft.EntityFrameworkCore.Migrations;

namespace BankApp.Services.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TransactionId",
                schema: "practice",
                table: "Transactions",
                type: "varchar(90)",
                maxLength: 90,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TransactionId",
                schema: "practice",
                table: "Transactions",
                type: "varchar(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(90)",
                oldMaxLength: 90);
        }
    }
}
