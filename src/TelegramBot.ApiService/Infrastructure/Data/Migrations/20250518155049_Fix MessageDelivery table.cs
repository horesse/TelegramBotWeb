using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TelegramBot.ApiService.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixMessageDeliverytable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageDeliveries_Chats_ChatId1",
                schema: "public",
                table: "MessageDeliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageDeliveries_Messages_MessageId1",
                schema: "public",
                table: "MessageDeliveries");

            migrationBuilder.DropIndex(
                name: "IX_MessageDeliveries_ChatId1",
                schema: "public",
                table: "MessageDeliveries");

            migrationBuilder.DropIndex(
                name: "IX_MessageDeliveries_MessageId1",
                schema: "public",
                table: "MessageDeliveries");

            migrationBuilder.DropColumn(
                name: "ChatId1",
                schema: "public",
                table: "MessageDeliveries");

            migrationBuilder.DropColumn(
                name: "MessageId1",
                schema: "public",
                table: "MessageDeliveries");

            migrationBuilder.AlterColumn<int>(
                name: "MessageId",
                schema: "public",
                table: "MessageDeliveries",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "ChatId",
                schema: "public",
                table: "MessageDeliveries",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_MessageDeliveries_ChatId",
                schema: "public",
                table: "MessageDeliveries",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageDeliveries_MessageId",
                schema: "public",
                table: "MessageDeliveries",
                column: "MessageId");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageDeliveries_Chats_ChatId",
                schema: "public",
                table: "MessageDeliveries",
                column: "ChatId",
                principalSchema: "public",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MessageDeliveries_Messages_MessageId",
                schema: "public",
                table: "MessageDeliveries",
                column: "MessageId",
                principalSchema: "public",
                principalTable: "Messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageDeliveries_Chats_ChatId",
                schema: "public",
                table: "MessageDeliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageDeliveries_Messages_MessageId",
                schema: "public",
                table: "MessageDeliveries");

            migrationBuilder.DropIndex(
                name: "IX_MessageDeliveries_ChatId",
                schema: "public",
                table: "MessageDeliveries");

            migrationBuilder.DropIndex(
                name: "IX_MessageDeliveries_MessageId",
                schema: "public",
                table: "MessageDeliveries");

            migrationBuilder.AlterColumn<long>(
                name: "MessageId",
                schema: "public",
                table: "MessageDeliveries",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<long>(
                name: "ChatId",
                schema: "public",
                table: "MessageDeliveries",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "ChatId1",
                schema: "public",
                table: "MessageDeliveries",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MessageId1",
                schema: "public",
                table: "MessageDeliveries",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MessageDeliveries_ChatId1",
                schema: "public",
                table: "MessageDeliveries",
                column: "ChatId1");

            migrationBuilder.CreateIndex(
                name: "IX_MessageDeliveries_MessageId1",
                schema: "public",
                table: "MessageDeliveries",
                column: "MessageId1");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageDeliveries_Chats_ChatId1",
                schema: "public",
                table: "MessageDeliveries",
                column: "ChatId1",
                principalSchema: "public",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MessageDeliveries_Messages_MessageId1",
                schema: "public",
                table: "MessageDeliveries",
                column: "MessageId1",
                principalSchema: "public",
                principalTable: "Messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
