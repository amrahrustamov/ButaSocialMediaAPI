using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ButaAPI.Migrations
{
    public partial class Implement_TKey_Fields_For_Whole_Entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FriendshipId",
                table: "Friendships",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Friendships",
                newName: "FriendshipId");
        }
    }
}
