using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NGA.Data.Migrations
{
    public partial class T10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ToUserId",
                table: "Message",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsOneToOneChat",
                table: "Group",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ToUserId",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "IsOneToOneChat",
                table: "Group");
        }
    }
}
