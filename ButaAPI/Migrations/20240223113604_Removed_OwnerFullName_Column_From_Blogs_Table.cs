using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ButaAPI.Migrations
{
    public partial class Removed_OwnerFullName_Column_From_Blogs_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerFullName",
                table: "Blogs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerFullName",
                table: "Blogs",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
