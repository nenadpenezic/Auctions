using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class ItemEntityRelationshipsCorrection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AcceptedOfferID",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_AcceptedOfferID",
                table: "Items",
                column: "AcceptedOfferID",
                unique: true,
                filter: "[AcceptedOfferID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Offers_AcceptedOfferID",
                table: "Items",
                column: "AcceptedOfferID",
                principalTable: "Offers",
                principalColumn: "OfferID");
        }
    }
}
