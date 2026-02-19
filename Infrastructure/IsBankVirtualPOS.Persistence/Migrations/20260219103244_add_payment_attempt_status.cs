using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IsBankVirtualPOS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class add_payment_attempt_status : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CardHolder",
                table: "PaymentAttempts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CardLast4",
                table: "PaymentAttempts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "PaymentAttempts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardHolder",
                table: "PaymentAttempts");

            migrationBuilder.DropColumn(
                name: "CardLast4",
                table: "PaymentAttempts");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "PaymentAttempts");
        }
    }
}
