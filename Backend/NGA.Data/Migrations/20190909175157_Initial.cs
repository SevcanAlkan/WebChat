using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NGA.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreateDT = table.Column<DateTime>(nullable: false),
                    UpdateDT = table.Column<DateTime>(nullable: true),
                    CreateBy = table.Column<Guid>(nullable: false),
                    UpdateBy = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    IsMain = table.Column<bool>(nullable: false, defaultValue: false),
                    IsPrivate = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    ResponseTime = table.Column<int>(nullable: false),
                    ControllerName = table.Column<string>(maxLength: 50, nullable: true),
                    ActionName = table.Column<string>(maxLength: 50, nullable: true),
                    ReturnTypeName = table.Column<string>(maxLength: 250, nullable: true),
                    Path = table.Column<string>(maxLength: 250, nullable: true),
                    MethodType = table.Column<byte>(nullable: false),
                    RequestBody = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parameter",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreateDT = table.Column<DateTime>(nullable: false),
                    UpdateDT = table.Column<DateTime>(nullable: true),
                    CreateBy = table.Column<Guid>(nullable: false),
                    UpdateBy = table.Column<Guid>(nullable: true),
                    Code = table.Column<string>(maxLength: 100, nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    GroupCode = table.Column<string>(maxLength: 10, nullable: true),
                    Value = table.Column<string>(nullable: true),
                    OrderIndex = table.Column<int>(nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parameter", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreateDT = table.Column<DateTime>(nullable: false),
                    UpdateDT = table.Column<DateTime>(nullable: true),
                    CreateBy = table.Column<Guid>(nullable: false),
                    UpdateBy = table.Column<Guid>(nullable: true),
                    UserName = table.Column<string>(maxLength: 15, nullable: false),
                    PaswordHash = table.Column<string>(maxLength: 50, nullable: false),
                    LastLoginDateTime = table.Column<DateTime>(nullable: true),
                    IsAdmin = table.Column<bool>(nullable: false, defaultValue: false),
                    IsBanned = table.Column<bool>(nullable: false, defaultValue: false),
                    DisplayName = table.Column<string>(maxLength: 20, nullable: false),
                    About = table.Column<string>(maxLength: 250, nullable: true),
                    Status = table.Column<byte>(nullable: false, defaultValue: (byte)4)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LogErrors",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OrderNum = table.Column<int>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false),
                    StackTrace = table.Column<string>(nullable: true),
                    Source = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    InnerException = table.Column<string>(nullable: true),
                    RequestId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogErrors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogErrors_Logs_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Logs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreateDT = table.Column<DateTime>(nullable: false),
                    UpdateDT = table.Column<DateTime>(nullable: true),
                    CreateBy = table.Column<Guid>(nullable: false),
                    UpdateBy = table.Column<Guid>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    GroupId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupUser_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupUser_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreateDT = table.Column<DateTime>(nullable: false),
                    UpdateDT = table.Column<DateTime>(nullable: true),
                    CreateBy = table.Column<Guid>(nullable: false),
                    UpdateBy = table.Column<Guid>(nullable: true),
                    Text = table.Column<string>(maxLength: 500, nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    GroupId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Message_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Message_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupUser_GroupId",
                table: "GroupUser",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupUser_UserId",
                table: "GroupUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LogErrors_RequestId",
                table: "LogErrors",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_GroupId",
                table: "Message",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_UserId",
                table: "Message",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupUser");

            migrationBuilder.DropTable(
                name: "LogErrors");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "Parameter");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
