using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GrandTravelPackages.Migrations
{
    public partial class RemoveMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PackageTbl_TravelProviderTbl_TravelProviderId",
                table: "PackageTbl");

            migrationBuilder.DropIndex(
                name: "IX_PackageTbl_TravelProviderId",
                table: "PackageTbl");

            migrationBuilder.DropColumn(
                name: "TravelProviderId",
                table: "PackageTbl");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TravelProviderId",
                table: "PackageTbl",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PackageTbl_TravelProviderId",
                table: "PackageTbl",
                column: "TravelProviderId");

            migrationBuilder.AddForeignKey(
                name: "FK_PackageTbl_TravelProviderTbl_TravelProviderId",
                table: "PackageTbl",
                column: "TravelProviderId",
                principalTable: "TravelProviderTbl",
                principalColumn: "TravelProviderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
