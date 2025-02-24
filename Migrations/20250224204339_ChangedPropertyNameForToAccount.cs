using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankApp.Migrations
{
    /// <inheritdoc />
    public partial class ChangedPropertyNameForToAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ToAccount",
                table: "BankTransactions");

            migrationBuilder.AddColumn<int>(
                name: "ToAccountId",
                table: "BankTransactions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ToAccountId",
                table: "BankTransactions");

            migrationBuilder.AddColumn<string>(
                name: "ToAccount",
                table: "BankTransactions",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
