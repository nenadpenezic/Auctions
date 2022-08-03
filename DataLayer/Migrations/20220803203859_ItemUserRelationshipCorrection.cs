using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class ItemUserRelationshipCorrection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Users_OwnerUserID",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_OwnerUserID",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "OwnerUserID",
                table: "Items");

            migrationBuilder.AddColumn<int>(
                name: "OwnerID",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Items_OwnerID",
                table: "Items",
                column: "OwnerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Users_OwnerID",
                table: "Items",
                column: "OwnerID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Users_OwnerID",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_OwnerID",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "OwnerID",
                table: "Items");

            migrationBuilder.AddColumn<int>(
                name: "OwnerUserID",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_OwnerUserID",
                table: "Items",
                column: "OwnerUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Users_OwnerUserID",
                table: "Items",
                column: "OwnerUserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
