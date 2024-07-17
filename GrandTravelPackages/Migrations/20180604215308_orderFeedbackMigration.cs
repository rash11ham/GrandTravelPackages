using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GrandTravelPackages.Migrations
{
    public partial class orderFeedbackMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Feedback",
                table: "PackageTbl");

            migrationBuilder.RenameColumn(
                name: "PackagId",
                table: "OrderTbl",
                newName: "PackagId");

            migrationBuilder.AddColumn<string>(
                name: "Feedback",
                table: "OrderTbl",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Feedback",
                table: "OrderTbl");

            migrationBuilder.RenameColumn(
                name: "PackagId",
                table: "OrderTbl",
                newName: "PackagId");

            migrationBuilder.AddColumn<string>(
                name: "Feedback",
                table: "PackageTbl",
                nullable: true);
        }
    }
}
