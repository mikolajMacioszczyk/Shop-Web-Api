using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopApi.DAL.Migrations
{
    public partial class AddPeopleTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeItems_AddressItems_AddressId",
                table: "EmployeeItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_CustomerItems_CustomerId",
                table: "OrderItems");

            migrationBuilder.DropTable(
                name: "CustomerItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeItems",
                table: "EmployeeItems");

            migrationBuilder.RenameTable(
                name: "EmployeeItems",
                newName: "PeopleItems");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeItems_AddressId",
                table: "PeopleItems",
                newName: "IX_PeopleItems_AddressId");

            migrationBuilder.AlterColumn<double>(
                name: "Salary",
                table: "PeopleItems",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "Permission",
                table: "PeopleItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "JobTitles",
                table: "PeopleItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfEmployment",
                table: "PeopleItems",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "PeopleItems",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "PeopleItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PeopleItems",
                table: "PeopleItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_PeopleItems_CustomerId",
                table: "OrderItems",
                column: "CustomerId",
                principalTable: "PeopleItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PeopleItems_AddressItems_AddressId",
                table: "PeopleItems",
                column: "AddressId",
                principalTable: "AddressItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_PeopleItems_CustomerId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PeopleItems_AddressItems_AddressId",
                table: "PeopleItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PeopleItems",
                table: "PeopleItems");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "PeopleItems");

            migrationBuilder.RenameTable(
                name: "PeopleItems",
                newName: "EmployeeItems");

            migrationBuilder.RenameIndex(
                name: "IX_PeopleItems_AddressId",
                table: "EmployeeItems",
                newName: "IX_EmployeeItems_AddressId");

            migrationBuilder.AlterColumn<double>(
                name: "Salary",
                table: "EmployeeItems",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Permission",
                table: "EmployeeItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "JobTitles",
                table: "EmployeeItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfEmployment",
                table: "EmployeeItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "EmployeeItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeItems",
                table: "EmployeeItems",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CustomerItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerItems_AddressItems_AddressId",
                        column: x => x.AddressId,
                        principalTable: "AddressItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerItems_AddressId",
                table: "CustomerItems",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeItems_AddressItems_AddressId",
                table: "EmployeeItems",
                column: "AddressId",
                principalTable: "AddressItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_CustomerItems_CustomerId",
                table: "OrderItems",
                column: "CustomerId",
                principalTable: "CustomerItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
