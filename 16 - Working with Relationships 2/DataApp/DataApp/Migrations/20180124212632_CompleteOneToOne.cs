using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataApp.Migrations
{
    public partial class CompleteOneToOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_ContactDetails_ContactId",
                table: "Suppliers");

            migrationBuilder.DropIndex(
                name: "IX_Suppliers_ContactId",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "ContactId",
                table: "Suppliers");

            migrationBuilder.AddColumn<long>(
                name: "SupplierId",
                table: "ContactDetails",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_ContactDetails_SupplierId",
                table: "ContactDetails",
                column: "SupplierId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactDetails_Suppliers_SupplierId",
                table: "ContactDetails",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactDetails_Suppliers_SupplierId",
                table: "ContactDetails");

            migrationBuilder.DropIndex(
                name: "IX_ContactDetails_SupplierId",
                table: "ContactDetails");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "ContactDetails");

            migrationBuilder.AddColumn<long>(
                name: "ContactId",
                table: "Suppliers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_ContactId",
                table: "Suppliers",
                column: "ContactId");

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_ContactDetails_ContactId",
                table: "Suppliers",
                column: "ContactId",
                principalTable: "ContactDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
