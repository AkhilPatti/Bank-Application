using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BankApp.Services.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "practice");

            migrationBuilder.CreateTable(
                name: "Banks",
                schema: "practice",
                columns: table => new
                {
                    BankId = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: false),
                    BankName = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false),
                    sImps = table.Column<double>(type: "double", nullable: false),
                    oImps = table.Column<double>(type: "double", nullable: false),
                    sRtgs = table.Column<double>(type: "double", nullable: false),
                    oRtgs = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.BankId);
                });

            migrationBuilder.CreateTable(
                name: "currencies",
                schema: "practice",
                columns: table => new
                {
                    CurrencyCode = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: false),
                    ExchangeRate = table.Column<double>(type: "double", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_currencies", x => x.CurrencyCode);
                });

            migrationBuilder.CreateTable(
                name: "StaffAccounts",
                schema: "practice",
                columns: table => new
                {
                    StaffId = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false),
                    Name = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    BankId = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffAccounts", x => x.StaffId);
                    table.ForeignKey(
                        name: "FK_StaffAccounts_Banks_StaffId",
                        column: x => x.StaffId,
                        principalSchema: "practice",
                        principalTable: "Banks",
                        principalColumn: "BankId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAccounts",
                schema: "practice",
                columns: table => new
                {
                    AccountId = table.Column<string>(type: "varchar(767)", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Pin = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    Balance = table.Column<float>(type: "float", nullable: false),
                    BankId = table.Column<string>(type: "varchar(12)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccounts", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_UserAccounts_Banks_BankId",
                        column: x => x.BankId,
                        principalSchema: "practice",
                        principalTable: "Banks",
                        principalColumn: "BankId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BankCurrencies",
                schema: "practice",
                columns: table => new
                {
                    BankId = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false),
                    CurrencyCode = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankCurrencies", x => new { x.BankId, x.CurrencyCode });
                    table.ForeignKey(
                        name: "FK_BankCurrencies_Banks_BankId",
                        column: x => x.BankId,
                        principalSchema: "practice",
                        principalTable: "Banks",
                        principalColumn: "BankId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BankCurrencies_currencies_CurrencyCode",
                        column: x => x.CurrencyCode,
                        principalSchema: "practice",
                        principalTable: "currencies",
                        principalColumn: "CurrencyCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                schema: "practice",
                columns: table => new
                {
                    TransactionId = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false),
                    SenderId = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, defaultValue: ""),
                    ReceiverId = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false, defaultValue: ""),
                    Amount = table.Column<float>(type: "float", nullable: false),
                    TransactionType = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transactions_UserAccounts_ReceiverId",
                        column: x => x.ReceiverId,
                        principalSchema: "practice",
                        principalTable: "UserAccounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_UserAccounts_SenderId",
                        column: x => x.SenderId,
                        principalSchema: "practice",
                        principalTable: "UserAccounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankCurrencies_CurrencyCode",
                schema: "practice",
                table: "BankCurrencies",
                column: "CurrencyCode");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ReceiverId",
                schema: "practice",
                table: "Transactions",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_SenderId",
                schema: "practice",
                table: "Transactions",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccounts_BankId",
                schema: "practice",
                table: "UserAccounts",
                column: "BankId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankCurrencies",
                schema: "practice");

            migrationBuilder.DropTable(
                name: "StaffAccounts",
                schema: "practice");

            migrationBuilder.DropTable(
                name: "Transactions",
                schema: "practice");

            migrationBuilder.DropTable(
                name: "currencies",
                schema: "practice");

            migrationBuilder.DropTable(
                name: "UserAccounts",
                schema: "practice");

            migrationBuilder.DropTable(
                name: "Banks",
                schema: "practice");
        }
    }
}
