using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopApi.DAL.Migrations
{
    public partial class AddFurnitureTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Furniture_CollectionItems_CollectionId",
                table: "Furniture");

            migrationBuilder.DropForeignKey(
                name: "FK_Furniture_OrderItems_OrderId",
                table: "Furniture");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Furniture",
                table: "Furniture");

            migrationBuilder.RenameTable(
                name: "Furniture",
                newName: "FurnitureItems");

            migrationBuilder.RenameIndex(
                name: "IX_Furniture_OrderId",
                table: "FurnitureItems",
                newName: "IX_FurnitureItems_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Furniture_CollectionId",
                table: "FurnitureItems",
                newName: "IX_FurnitureItems_CollectionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FurnitureItems",
                table: "FurnitureItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FurnitureItems_CollectionItems_CollectionId",
                table: "FurnitureItems",
                column: "CollectionId",
                principalTable: "CollectionItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FurnitureItems_OrderItems_OrderId",
                table: "FurnitureItems",
                column: "OrderId",
                principalTable: "OrderItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FurnitureItems_CollectionItems_CollectionId",
                table: "FurnitureItems");

            migrationBuilder.DropForeignKey(
                name: "FK_FurnitureItems_OrderItems_OrderId",
                table: "FurnitureItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FurnitureItems",
                table: "FurnitureItems");

            migrationBuilder.RenameTable(
                name: "FurnitureItems",
                newName: "Furniture");

            migrationBuilder.RenameIndex(
                name: "IX_FurnitureItems_OrderId",
                table: "Furniture",
                newName: "IX_Furniture_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_FurnitureItems_CollectionId",
                table: "Furniture",
                newName: "IX_Furniture_CollectionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Furniture",
                table: "Furniture",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Furniture_CollectionItems_CollectionId",
                table: "Furniture",
                column: "CollectionId",
                principalTable: "CollectionItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Furniture_OrderItems_OrderId",
                table: "Furniture",
                column: "OrderId",
                principalTable: "OrderItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
