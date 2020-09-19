using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopApi.DAL.Migrations
{
    public partial class AddIdColumnToFurnitureCountsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FurnitureCounts",
                table: "FurnitureCounts");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "FurnitureCounts",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FurnitureCounts",
                table: "FurnitureCounts",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_FurnitureCounts_FurnitureId",
                table: "FurnitureCounts",
                column: "FurnitureId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FurnitureCounts",
                table: "FurnitureCounts");

            migrationBuilder.DropIndex(
                name: "IX_FurnitureCounts_FurnitureId",
                table: "FurnitureCounts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "FurnitureCounts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FurnitureCounts",
                table: "FurnitureCounts",
                columns: new[] { "FurnitureId", "Count" });
        }
    }
}
