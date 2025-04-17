using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restino.Identity.Migrations
{
    /// <inheritdoc />
    public partial class AddingTwoFactorCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TwoFactorCode",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TwoFactorCodeDuration",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TwoFactorCode",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TwoFactorCodeDuration",
                table: "AspNetUsers");
        }
    }
}
