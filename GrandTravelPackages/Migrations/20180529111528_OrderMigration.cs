using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GrandTravelPackages.Migrations
{
    public partial class OrderMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PackageTbl",
                table: "PackageTbl");

            migrationBuilder.RenameTable(
                name: "PackageTbl",
                newName: "Package");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Package",
                table: "Package",
                column: "PackageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Package",
                table: "Package");

            migrationBuilder.RenameTable(
                name: "Package",
                newName: "PackageTbl");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PackageTbl",
                table: "PackageTbl",
                column: "PackageId");
        }
    }
}
