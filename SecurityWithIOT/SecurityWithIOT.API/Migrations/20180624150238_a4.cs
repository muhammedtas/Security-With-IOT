using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SecurityWithIOT.API.Migrations
{
    public partial class a4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Firstname",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lastname",
                table: "User",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NationalIdentificationNumber",
                table: "User",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Firstname",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Lastname",
                table: "User");

            migrationBuilder.DropColumn(
                name: "NationalIdentificationNumber",
                table: "User");
        }
    }
}
