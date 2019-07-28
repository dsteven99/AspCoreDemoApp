﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace AspCoreDemoApp.Data.Migrations
{
    public partial class addduration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "Videos",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Videos");
        }
    }
}
