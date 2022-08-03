using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class OfferTableCorrection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_AuctionParticipants_ItemAuctionParticipantUserID_ItemAuctionParticipantItemID",
                table: "Offers");

            migrationBuilder.RenameColumn(
                name: "ItemAuctionParticipantUserID",
                table: "Offers",
                newName: "ItemAuctionParticipantUserID1");

            migrationBuilder.RenameColumn(
                name: "ItemAuctionParticipantItemID",
                table: "Offers",
                newName: "ItemAuctionParticipantItemID1");

            migrationBuilder.RenameIndex(
                name: "IX_Offers_ItemAuctionParticipantUserID_ItemAuctionParticipantItemID",
                table: "Offers",
                newName: "IX_Offers_ItemAuctionParticipantUserID1_ItemAuctionParticipantItemID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_AuctionParticipants_ItemAuctionParticipantUserID1_ItemAuctionParticipantItemID1",
                table: "Offers",
                columns: new[] { "ItemAuctionParticipantUserID1", "ItemAuctionParticipantItemID1" },
                principalTable: "AuctionParticipants",
                principalColumns: new[] { "UserID", "ItemID" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_AuctionParticipants_ItemAuctionParticipantUserID1_ItemAuctionParticipantItemID1",
                table: "Offers");

            migrationBuilder.RenameColumn(
                name: "ItemAuctionParticipantUserID1",
                table: "Offers",
                newName: "ItemAuctionParticipantUserID");

            migrationBuilder.RenameColumn(
                name: "ItemAuctionParticipantItemID1",
                table: "Offers",
                newName: "ItemAuctionParticipantItemID");

            migrationBuilder.RenameIndex(
                name: "IX_Offers_ItemAuctionParticipantUserID1_ItemAuctionParticipantItemID1",
                table: "Offers",
                newName: "IX_Offers_ItemAuctionParticipantUserID_ItemAuctionParticipantItemID");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_AuctionParticipants_ItemAuctionParticipantUserID_ItemAuctionParticipantItemID",
                table: "Offers",
                columns: new[] { "ItemAuctionParticipantUserID", "ItemAuctionParticipantItemID" },
                principalTable: "AuctionParticipants",
                principalColumns: new[] { "UserID", "ItemID" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
