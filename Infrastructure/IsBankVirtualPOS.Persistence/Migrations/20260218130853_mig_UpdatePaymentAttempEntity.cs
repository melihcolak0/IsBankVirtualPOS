using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IsBankVirtualPOS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_UpdatePaymentAttempEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthCode",
                table: "PaymentAttempts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BankReferenceNumber",
                table: "PaymentAttempts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RawRequest",
                table: "PaymentAttempts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RawResponse",
                table: "PaymentAttempts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthCode",
                table: "PaymentAttempts");

            migrationBuilder.DropColumn(
                name: "BankReferenceNumber",
                table: "PaymentAttempts");

            migrationBuilder.DropColumn(
                name: "RawRequest",
                table: "PaymentAttempts");

            migrationBuilder.DropColumn(
                name: "RawResponse",
                table: "PaymentAttempts");
        }
    }
}
