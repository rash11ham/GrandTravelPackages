using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GrandTravelPackages.Migrations
{
    public partial class FeedbackMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderTbl",
                table: "OrderTbl");

            migrationBuilder.DropColumn(
                name: "Feedback",
                table: "OrderTbl");

            migrationBuilder.RenameTable(
                name: "OrderTbl",
                newName: "Order");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "OrderTbl");

            migrationBuilder.AddColumn<string>(
                name: "Feedback",
                table: "OrderTbl",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderTbl",
                table: "OrderTbl",
                column: "OrderId");
        }
    }
}
