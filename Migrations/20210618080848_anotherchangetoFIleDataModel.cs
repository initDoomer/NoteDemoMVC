using Microsoft.EntityFrameworkCore.Migrations;

namespace NoteDemoMVC.Migrations
{
    public partial class anotherchangetoFIleDataModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "FileData",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "FileData");
        }
    }
}
