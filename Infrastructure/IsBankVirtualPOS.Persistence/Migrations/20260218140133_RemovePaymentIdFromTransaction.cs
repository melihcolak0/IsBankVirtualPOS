using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IsBankVirtualPOS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemovePaymentIdFromTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
        name: "FK_PaymentTransactions_Payments_PaymentId",
        table: "PaymentTransactions");

            migrationBuilder.DropIndex(
                name: "IX_PaymentTransactions_PaymentId",
                table: "PaymentTransactions");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "PaymentTransactions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
        name: "PaymentId",
        table: "PaymentTransactions",
        type: "uniqueidentifier",
        nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransactions_PaymentId",
                table: "PaymentTransactions",
                column: "PaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentTransactions_Payments_PaymentId",
                table: "PaymentTransactions",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id");
        }
    }
}
