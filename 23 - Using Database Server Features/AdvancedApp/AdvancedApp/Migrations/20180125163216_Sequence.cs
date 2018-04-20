using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AdvancedApp.Migrations
{
    public partial class Sequence : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "ReferenceSequence",
                startValue: 100L,
                incrementBy: 2);

            migrationBuilder.AlterColumn<string>(
                name: "GeneratedValue",
                table: "Employees",
                nullable: true,
                defaultValueSql: @"'REFERENCE_' 
                    + CONVERT(varchar, NEXT VALUE FOR ReferenceSequence)",
                oldClrType: typeof(string),
                oldNullable: true,
                oldDefaultValueSql: "GETDATE()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropSequence(
                name: "ReferenceSequence");

            migrationBuilder.AlterColumn<string>(
                name: "GeneratedValue",
                table: "Employees",
                nullable: true,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(string),
                oldNullable: true,
                oldDefaultValueSql: @"'REFERENCE_' 
                    + CONVERT(varchar, NEXT VALUE FOR ReferenceSequence)");
        }
    }
}
