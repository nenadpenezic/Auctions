using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class AddedContactEmailColumnInUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JMBG",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "EmailForContact",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailForContact",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "JMBG",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
