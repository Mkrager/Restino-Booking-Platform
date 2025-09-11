using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restino.Identity.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailPropertyToUserResetPasswordCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "UserResetPasswordCodes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "UserResetPasswordCodes");
        }
    }
}
