using Microsoft.EntityFrameworkCore.Migrations;

namespace NoteDemoMVC.Migrations
{
    public partial class filedataModelchange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "FileData",
                newName: "ImageName");

            migrationBuilder.AddColumn<string>(
                name: "ImageCaption",
                table: "FileData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageDescription",
                table: "FileData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageCaption",
                table: "FileData");

            migrationBuilder.DropColumn(
                name: "ImageDescription",
                table: "FileData");

            migrationBuilder.RenameColumn(
                name: "ImageName",
                table: "FileData",
                newName: "FileName");
        }
    }
}
