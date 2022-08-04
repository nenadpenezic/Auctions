using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class OfferTableKeyCorrection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_AuctionParticipants_ItemID_UserID",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_Offers_ItemID_UserID",
                table: "Offers");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_UserID_ItemID",
                table: "Offers",
                columns: new[] { "UserID", "ItemID" });

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_AuctionParticipants_UserID_ItemID",
                table: "Offers",
                columns: new[] { "UserID", "ItemID" },
                principalTable: "AuctionParticipants",
                principalColumns: new[] { "UserID", "ItemID" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_AuctionParticipants_UserID_ItemID",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_Offers_UserID_ItemID",
                table: "Offers");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_ItemID_UserID",
                table: "Offers",
                columns: new[] { "ItemID", "UserID" });

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_AuctionParticipants_ItemID_UserID",
                table: "Offers",
                columns: new[] { "ItemID", "UserID" },
                principalTable: "AuctionParticipants",
                principalColumns: new[] { "UserID", "ItemID" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
