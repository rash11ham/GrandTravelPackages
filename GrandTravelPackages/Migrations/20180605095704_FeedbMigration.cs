using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GrandTravelPackages.Migrations
{
    public partial class FeedbMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "OrderTbl");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderTbl",
                table: "OrderTbl",
                column: "OrderId");

            migrationBuilder.CreateTable(
                name: "FeedbackTbl",
                columns: table => new
                {
                    FeedbackId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OrdFeedback = table.Column<string>(nullable: true),
                    OrderId = table.Column<int>(nullable: false),
                    PackDescription = table.Column<string>(nullable: true),
                    PackImage = table.Column<string>(nullable: true),
                    PackLocation = table.Column<string>(nullable: true),
                    PackName = table.Column<string>(nullable: true),
                    PackPrice = table.Column<decimal>(nullable: false),
                    PackageId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedbackTbl", x => x.FeedbackId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeedbackTbl");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderTbl",
                table: "OrderTbl");

            migrationBuilder.RenameTable(
                name: "OrderTbl",
                newName: "Order");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "OrderId");
        }
    }
}
