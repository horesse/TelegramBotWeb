using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TelegramBot.ApiService.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Addtokenfield : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                schema: "public",
                table: "Settings");

            migrationBuilder.AddColumn<string>(
                name: "Token",
                schema: "public",
                table: "Settings",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                schema: "public",
                table: "Settings");

            migrationBuilder.AddColumn<string>(
                name: "Value",
                schema: "public",
                table: "Settings",
                type: "text",
                nullable: true);
        }
    }
}
