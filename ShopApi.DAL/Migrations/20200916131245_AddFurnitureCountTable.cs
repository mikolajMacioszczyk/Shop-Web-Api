using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopApi.DAL.Migrations
{
    public partial class AddFurnitureCountTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FurnitureItems_OrderItems_OrderId",
                table: "FurnitureItems");

            migrationBuilder.DropIndex(
                name: "IX_FurnitureItems_OrderId",
                table: "FurnitureItems");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "FurnitureItems");

            migrationBuilder.CreateTable(
                name: "FurnitureCounts",
                columns: table => new
                {
                    FurnitureId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FurnitureCounts", x => new { x.FurnitureId, x.Count });
                    table.ForeignKey(
                        name: "FK_FurnitureCounts_FurnitureItems_FurnitureId",
                        column: x => x.FurnitureId,
                        principalTable: "FurnitureItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FurnitureCounts_OrderItems_OrderId",
                        column: x => x.OrderId,
                        principalTable: "OrderItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FurnitureCounts_OrderId",
                table: "FurnitureCounts",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FurnitureCounts");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "FurnitureItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FurnitureItems_OrderId",
                table: "FurnitureItems",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_FurnitureItems_OrderItems_OrderId",
                table: "FurnitureItems",
                column: "OrderId",
                principalTable: "OrderItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
