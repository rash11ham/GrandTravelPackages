using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GrandTravelPackages.Migrations
{
    public partial class EditOrderMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PackDescription",
                table: "OrderTbl");

            migrationBuilder.DropColumn(
                name: "PackImage",
                table: "OrderTbl");

            migrationBuilder.DropColumn(
                name: "PackLocation",
                table: "OrderTbl");

            migrationBuilder.RenameColumn(
                name: "PackPrice",
                table: "OrderTbl",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "PackName",
                table: "OrderTbl",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "OrderDate",
                table: "OrderTbl",
                newName: "Date");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "OrderTbl",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "OrderTbl",
                newName: "PackName");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "OrderTbl",
                newName: "PackPrice");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "OrderTbl",
                newName: "OrderDate");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "OrderTbl",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "PackDescription",
                table: "OrderTbl",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PackImage",
                table: "OrderTbl",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PackLocation",
                table: "OrderTbl",
                nullable: true);
        }
    }
}
