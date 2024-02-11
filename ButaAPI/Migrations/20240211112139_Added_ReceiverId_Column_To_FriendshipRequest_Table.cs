using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ButaAPI.Migrations
{
    public partial class Added_ReceiverId_Column_To_FriendshipRequest_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReceiverId",
                table: "FriendshipRequest",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiverId",
                table: "FriendshipRequest");
        }
    }
}
