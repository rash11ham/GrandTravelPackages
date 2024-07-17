using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GrandTravelPackages.Migrations
{
    public partial class order2Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<string>(
                name: "PackName",
                table: "OrderTbl",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PackPrice",
                table: "OrderTbl",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "PackName",
                table: "OrderTbl");

            migrationBuilder.DropColumn(
                name: "PackPrice",
                table: "OrderTbl");
        }
    }
}
