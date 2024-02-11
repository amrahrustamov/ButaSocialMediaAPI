using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ButaAPI.Migrations
{
    public partial class added_FriendshipRequest_To_DbSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendshipRequest_Users_SenderId",
                table: "FriendshipRequest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FriendshipRequest",
                table: "FriendshipRequest");

            migrationBuilder.RenameTable(
                name: "FriendshipRequest",
                newName: "FriendshipsRequests");

            migrationBuilder.RenameIndex(
                name: "IX_FriendshipRequest_SenderId",
                table: "FriendshipsRequests",
                newName: "IX_FriendshipsRequests_SenderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FriendshipsRequests",
                table: "FriendshipsRequests",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendshipsRequests_Users_SenderId",
                table: "FriendshipsRequests",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendshipsRequests_Users_SenderId",
                table: "FriendshipsRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FriendshipsRequests",
                table: "FriendshipsRequests");

            migrationBuilder.RenameTable(
                name: "FriendshipsRequests",
                newName: "FriendshipRequest");

            migrationBuilder.RenameIndex(
                name: "IX_FriendshipsRequests_SenderId",
                table: "FriendshipRequest",
                newName: "IX_FriendshipRequest_SenderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FriendshipRequest",
                table: "FriendshipRequest",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendshipRequest_Users_SenderId",
                table: "FriendshipRequest",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
