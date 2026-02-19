using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IsBankVirtualPOS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePaymentTransactionRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentTransactions_Payments_PaymentId",
                table: "PaymentTransactions");

            migrationBuilder.AlterColumn<Guid>(
                name: "PaymentId",
                table: "PaymentTransactions",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentAttemptId",
                table: "PaymentTransactions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransactions_PaymentAttemptId",
                table: "PaymentTransactions",
                column: "PaymentAttemptId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentTransactions_PaymentAttempts_PaymentAttemptId",
                table: "PaymentTransactions",
                column: "PaymentAttemptId",
                principalTable: "PaymentAttempts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentTransactions_Payments_PaymentId",
                table: "PaymentTransactions",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentTransactions_PaymentAttempts_PaymentAttemptId",
                table: "PaymentTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentTransactions_Payments_PaymentId",
                table: "PaymentTransactions");

            migrationBuilder.DropIndex(
                name: "IX_PaymentTransactions_PaymentAttemptId",
                table: "PaymentTransactions");

            migrationBuilder.DropColumn(
                name: "PaymentAttemptId",
                table: "PaymentTransactions");

            migrationBuilder.AlterColumn<Guid>(
                name: "PaymentId",
                table: "PaymentTransactions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentTransactions_Payments_PaymentId",
                table: "PaymentTransactions",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
