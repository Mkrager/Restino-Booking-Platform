using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restino.Identity.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailPropertyToUserTwoFactorCodeEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "UserTwoFactorCodes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "UserResetPasswordCodes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "UserTwoFactorCodes");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "UserResetPasswordCodes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
