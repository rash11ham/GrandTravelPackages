using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GrandTravelPackages.Migrations
{
    public partial class EnumMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "PackageTbl");

            migrationBuilder.AddColumn<int>(
                name: "PackageStatus",
                table: "PackageTbl",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PackageStatus",
                table: "PackageTbl");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "PackageTbl",
                nullable: false,
                defaultValue: false);
        }
    }
}
