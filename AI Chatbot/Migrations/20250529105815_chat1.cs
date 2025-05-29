using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AI_Chatbot.Migrations
{
    /// <inheritdoc />
    public partial class chat1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChatMessageId",
                table: "ChatResponses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SessionId",
                table: "ChatResponses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ChatResponses",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChatMessageId",
                table: "ChatResponses");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "ChatResponses");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ChatResponses");
        }
    }
}
