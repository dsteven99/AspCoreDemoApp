using Microsoft.EntityFrameworkCore.Migrations;

namespace AspCoreDemoApp.Data.Migrations
{
    public partial class deletedescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Videos");

            migrationBuilder.RenameColumn(
                name: "width",
                table: "Videos",
                newName: "Width");

            migrationBuilder.RenameColumn(
                name: "height",
                table: "Videos",
                newName: "Height");

            migrationBuilder.RenameColumn(
                name: "code",
                table: "Videos",
                newName: "Code");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Videos",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 250);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Width",
                table: "Videos",
                newName: "width");

            migrationBuilder.RenameColumn(
                name: "Height",
                table: "Videos",
                newName: "height");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Videos",
                newName: "code");

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "Videos",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Videos",
                nullable: true);
        }
    }
}
