using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IsBankVirtualPOS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_AddPaymentOpt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OtpCode",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OtpExpireAt",
                table: "Payments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "OtpVerified",
                table: "Payments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OtpCode",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "OtpExpireAt",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "OtpVerified",
                table: "Payments");
        }
    }
}
