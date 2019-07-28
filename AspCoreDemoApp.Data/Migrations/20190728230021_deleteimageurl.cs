using Microsoft.EntityFrameworkCore.Migrations;

namespace AspCoreDemoApp.Data.Migrations
{
    public partial class deleteimageurl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Videos");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Videos",
                nullable: true);
        }
    }
}
