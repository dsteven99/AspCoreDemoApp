using Microsoft.EntityFrameworkCore.Migrations;

namespace AspCoreDemoApp.Data.Migrations
{
    public partial class videofield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Videos");

            migrationBuilder.AddColumn<string>(
                name: "code",
                table: "Videos",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "height",
                table: "Videos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "width",
                table: "Videos",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "code",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "height",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "width",
                table: "Videos");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Videos",
                nullable: true);
        }
    }
}
