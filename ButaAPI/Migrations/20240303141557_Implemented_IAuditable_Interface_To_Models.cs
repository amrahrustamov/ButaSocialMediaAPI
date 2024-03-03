using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ButaAPI.Migrations
{
    public partial class Implemented_IAuditable_Interface_To_Models : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreateTime",
                table: "Users",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Notifications",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "SendingTime",
                table: "Messages",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Likes",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "FriendshipsRequests",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Comments",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Blogs",
                newName: "UpdatedAt");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Notifications",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Messages",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Likes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "FriendshipsRequests",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Comments",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Blogs",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "FriendshipsRequests");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Blogs");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Users",
                newName: "CreateTime");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Notifications",
                newName: "DateTime");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Messages",
                newName: "SendingTime");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Likes",
                newName: "DateTime");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "FriendshipsRequests",
                newName: "DateTime");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Comments",
                newName: "DateTime");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Blogs",
                newName: "DateTime");
        }
    }
}
