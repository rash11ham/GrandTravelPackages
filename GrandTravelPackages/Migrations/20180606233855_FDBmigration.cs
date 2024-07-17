using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GrandTravelPackages.Migrations
{
    public partial class FDBmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PackDescription",
                table: "FeedbackTbl");

            migrationBuilder.DropColumn(
                name: "PackImage",
                table: "FeedbackTbl");

            migrationBuilder.DropColumn(
                name: "PackLocation",
                table: "FeedbackTbl");

            migrationBuilder.DropColumn(
                name: "PackName",
                table: "FeedbackTbl");

            migrationBuilder.DropColumn(
                name: "PackPrice",
                table: "FeedbackTbl");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PackDescription",
                table: "FeedbackTbl",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PackImage",
                table: "FeedbackTbl",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PackLocation",
                table: "FeedbackTbl",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PackName",
                table: "FeedbackTbl",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PackPrice",
                table: "FeedbackTbl",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
