using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class ItemOfferRelationCorrection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AcceptedOfferID",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Items_AcceptedOfferID",
                table: "Items",
                column: "AcceptedOfferID");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Offers_AcceptedOfferID",
                table: "Items",
                column: "AcceptedOfferID",
                principalTable: "Offers",
                principalColumn: "OfferID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Offers_AcceptedOfferID",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_AcceptedOfferID",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "AcceptedOfferID",
                table: "Items");
        }
    }
}
