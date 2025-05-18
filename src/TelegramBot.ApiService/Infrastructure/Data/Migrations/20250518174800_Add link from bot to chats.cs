using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TelegramBot.ApiService.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Addlinkfrombottochats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BotId",
                schema: "public",
                table: "Chats",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Chats_BotId",
                schema: "public",
                table: "Chats",
                column: "BotId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Bots_BotId",
                schema: "public",
                table: "Chats",
                column: "BotId",
                principalSchema: "public",
                principalTable: "Bots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Bots_BotId",
                schema: "public",
                table: "Chats");

            migrationBuilder.DropIndex(
                name: "IX_Chats_BotId",
                schema: "public",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "BotId",
                schema: "public",
                table: "Chats");
        }
    }
}
