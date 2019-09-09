using Microsoft.EntityFrameworkCore.Migrations;

namespace NGA.Data.Migrations
{
    public partial class B1NGA5R2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animal_AnimalTypes_TypeId",
                table: "Animal");

            migrationBuilder.DropForeignKey(
                name: "FK_NestAnimals_Animal_AnimalId",
                table: "NestAnimals");

            migrationBuilder.DropForeignKey(
                name: "FK_NestAnimals_Nests_NestId",
                table: "NestAnimals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Parameters",
                table: "Parameters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Nests",
                table: "Nests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NestAnimals",
                table: "NestAnimals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnimalTypes",
                table: "AnimalTypes");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Parameters",
                newName: "Parameter");

            migrationBuilder.RenameTable(
                name: "Nests",
                newName: "Nest");

            migrationBuilder.RenameTable(
                name: "NestAnimals",
                newName: "NestAnimal");

            migrationBuilder.RenameTable(
                name: "AnimalTypes",
                newName: "AnimalType");

            migrationBuilder.RenameIndex(
                name: "IX_NestAnimals_NestId",
                table: "NestAnimal",
                newName: "IX_NestAnimal_NestId");

            migrationBuilder.RenameIndex(
                name: "IX_NestAnimals_AnimalId",
                table: "NestAnimal",
                newName: "IX_NestAnimal_AnimalId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Parameter",
                table: "Parameter",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Nest",
                table: "Nest",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NestAnimal",
                table: "NestAnimal",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnimalType",
                table: "AnimalType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Animal_AnimalType_TypeId",
                table: "Animal",
                column: "TypeId",
                principalTable: "AnimalType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NestAnimal_Animal_AnimalId",
                table: "NestAnimal",
                column: "AnimalId",
                principalTable: "Animal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NestAnimal_Nest_NestId",
                table: "NestAnimal",
                column: "NestId",
                principalTable: "Nest",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animal_AnimalType_TypeId",
                table: "Animal");

            migrationBuilder.DropForeignKey(
                name: "FK_NestAnimal_Animal_AnimalId",
                table: "NestAnimal");

            migrationBuilder.DropForeignKey(
                name: "FK_NestAnimal_Nest_NestId",
                table: "NestAnimal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Parameter",
                table: "Parameter");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NestAnimal",
                table: "NestAnimal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Nest",
                table: "Nest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnimalType",
                table: "AnimalType");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Parameter",
                newName: "Parameters");

            migrationBuilder.RenameTable(
                name: "NestAnimal",
                newName: "NestAnimals");

            migrationBuilder.RenameTable(
                name: "Nest",
                newName: "Nests");

            migrationBuilder.RenameTable(
                name: "AnimalType",
                newName: "AnimalTypes");

            migrationBuilder.RenameIndex(
                name: "IX_NestAnimal_NestId",
                table: "NestAnimals",
                newName: "IX_NestAnimals_NestId");

            migrationBuilder.RenameIndex(
                name: "IX_NestAnimal_AnimalId",
                table: "NestAnimals",
                newName: "IX_NestAnimals_AnimalId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Parameters",
                table: "Parameters",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NestAnimals",
                table: "NestAnimals",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Nests",
                table: "Nests",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnimalTypes",
                table: "AnimalTypes",
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

            migrationBuilder.AddForeignKey(
                name: "FK_NestAnimals_Nests_NestId",
                table: "NestAnimals",
                column: "NestId",
                principalTable: "Nests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
