using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataApp.Migrations
{
    public partial class AdditionalTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ContactId",
                table: "Supplier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ContactLocation",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(nullable: true),
                    LocationName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactLocation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactDetails",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LocationId = table.Column<long>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactDetails_ContactLocation_LocationId",
                        column: x => x.LocationId,
                        principalTable: "ContactLocation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_ContactId",
                table: "Supplier",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactDetails_LocationId",
                table: "ContactDetails",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Supplier_ContactDetails_ContactId",
                table: "Supplier",
                column: "ContactId",
                principalTable: "ContactDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Supplier_ContactDetails_ContactId",
                table: "Supplier");

            migrationBuilder.DropTable(
                name: "ContactDetails");

            migrationBuilder.DropTable(
                name: "ContactLocation");

            migrationBuilder.DropIndex(
                name: "IX_Supplier_ContactId",
                table: "Supplier");

            migrationBuilder.DropColumn(
                name: "ContactId",
                table: "Supplier");
        }
    }
}
