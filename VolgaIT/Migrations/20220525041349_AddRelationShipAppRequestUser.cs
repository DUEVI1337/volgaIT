using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VolgaIT.Migrations
{
    public partial class AddRelationShipAppRequestUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "RequestUsers",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "timestamp");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreate",
                table: "Apps",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "timestamp");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "CreatedDate",
                table: "RequestUsers",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<byte[]>(
                name: "DateCreate",
                table: "Apps",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
