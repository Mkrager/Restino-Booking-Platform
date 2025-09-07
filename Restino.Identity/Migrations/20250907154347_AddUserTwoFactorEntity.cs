using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restino.Identity.Migrations
{
    /// <inheritdoc />
    public partial class AddUserTwoFactorEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "UserTwoFactors",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserTwoFactors_CreatedBy",
                table: "UserTwoFactors",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTwoFactors_AspNetUsers_CreatedBy",
                table: "UserTwoFactors",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTwoFactors_AspNetUsers_CreatedBy",
                table: "UserTwoFactors");

            migrationBuilder.DropIndex(
                name: "IX_UserTwoFactors_CreatedBy",
                table: "UserTwoFactors");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "UserTwoFactors",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
