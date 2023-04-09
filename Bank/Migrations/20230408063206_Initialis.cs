using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bank.Migrations
{
    public partial class Initialis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BankHistory",
                table: "BankHistory");

            migrationBuilder.DeleteData(
                table: "BankAccounts",
                keyColumn: "CardNumber",
                keyValue: "5035033017235671");

            migrationBuilder.AlterColumn<string>(
                name: "FromCardNumber",
                table: "BankHistory",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "BankHistory",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_BankHistory",
                table: "BankHistory",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BankHistory",
                table: "BankHistory");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BankHistory");

            migrationBuilder.AlterColumn<string>(
                name: "FromCardNumber",
                table: "BankHistory",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BankHistory",
                table: "BankHistory",
                column: "FromCardNumber");

            migrationBuilder.InsertData(
                table: "BankAccounts",
                columns: new[] { "CardNumber", "Money", "PhoneNumber" },
                values: new object[] { "5035033017235671", 0m, "+97585436316" });
        }
    }
}
