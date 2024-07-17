using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GrandTravelPackages.Migrations
{
    public partial class Order1Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "OrderTbl",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    PackagID = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderTbl", x => x.OrderId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderTbl");

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
    }
}
