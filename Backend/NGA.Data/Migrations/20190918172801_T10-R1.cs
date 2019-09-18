using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NGA.Data.Migrations
{
    public partial class T10R1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "GroupUser");

            migrationBuilder.DropColumn(
                name: "CreateDT",
                table: "GroupUser");

            migrationBuilder.DropColumn(
                name: "UpdateBy",
                table: "GroupUser");

            migrationBuilder.DropColumn(
                name: "UpdateDT",
                table: "GroupUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreateBy",
                table: "GroupUser",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDT",
                table: "GroupUser",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateBy",
                table: "GroupUser",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDT",
                table: "GroupUser",
                nullable: true);
        }
    }
}
