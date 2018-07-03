using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SecurityWithIOT.API.Migrations
{
    public partial class a2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CountryCode",
                table: "Countries",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Latitude",
                table: "City",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Longitude",
                table: "City",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Population",
                table: "City",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "City",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "City");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "City");

            migrationBuilder.DropColumn(
                name: "Population",
                table: "City");

            migrationBuilder.DropColumn(
                name: "Region",
                table: "City");
        }
    }
}
