using Microsoft.EntityFrameworkCore.Migrations;

namespace NGA.Data.Migrations
{
    public partial class B1NGA5R1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_AnimalTypes_TypeId",
                table: "Animals");

            migrationBuilder.DropForeignKey(
                name: "FK_NestAnimals_Animals_AnimalId",
                table: "NestAnimals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Animals",
                table: "Animals");

            migrationBuilder.RenameTable(
                name: "Animals",
                newName: "Animal");

            migrationBuilder.RenameIndex(
                name: "IX_Animals_TypeId",
                table: "Animal",
                newName: "IX_Animal_TypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Animal",
                table: "Animal",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Animal_AnimalTypes_TypeId",
                table: "Animal",
                column: "TypeId",
                principalTable: "AnimalTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NestAnimals_Animal_AnimalId",
                table: "NestAnimals",
                column: "AnimalId",
                principalTable: "Animal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animal_AnimalTypes_TypeId",
                table: "Animal");

            migrationBuilder.DropForeignKey(
                name: "FK_NestAnimals_Animal_AnimalId",
                table: "NestAnimals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Animal",
                table: "Animal");

            migrationBuilder.RenameTable(
                name: "Animal",
                newName: "Animals");

            migrationBuilder.RenameIndex(
                name: "IX_Animal_TypeId",
                table: "Animals",
                newName: "IX_Animals_TypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Animals",
                table: "Animals",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_AnimalTypes_TypeId",
                table: "Animals",
                column: "TypeId",
                principalTable: "AnimalTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NestAnimals_Animals_AnimalId",
                table: "NestAnimals",
                column: "AnimalId",
                principalTable: "Animals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
