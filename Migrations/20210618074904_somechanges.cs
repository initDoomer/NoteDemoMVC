using Microsoft.EntityFrameworkCore.Migrations;

namespace NoteDemoMVC.Migrations
{
    public partial class somechanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "FileData");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "FileData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
