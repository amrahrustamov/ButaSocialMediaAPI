using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ButaAPI.Migrations
{
    public partial class Added_URL_Column_To_Notifications_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "URl",
                table: "Notifications",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "URl",
                table: "Notifications");
        }
    }
}
