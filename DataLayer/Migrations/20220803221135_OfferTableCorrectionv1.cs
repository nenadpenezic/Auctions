using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class OfferTableCorrectionv1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_AuctionParticipants_ItemAuctionParticipantUserID1_ItemAuctionParticipantItemID1",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_Offers_ItemAuctionParticipantUserID1_ItemAuctionParticipantItemID1",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "ItemAuctionParticipantItemID1",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "ItemAuctionParticipantUserID1",
                table: "Offers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemAuctionParticipantItemID1",
                table: "Offers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ItemAuctionParticipantUserID1",
                table: "Offers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Offers_ItemAuctionParticipantUserID1_ItemAuctionParticipantItemID1",
                table: "Offers",
                columns: new[] { "ItemAuctionParticipantUserID1", "ItemAuctionParticipantItemID1" });

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_AuctionParticipants_ItemAuctionParticipantUserID1_ItemAuctionParticipantItemID1",
                table: "Offers",
                columns: new[] { "ItemAuctionParticipantUserID1", "ItemAuctionParticipantItemID1" },
                principalTable: "AuctionParticipants",
                principalColumns: new[] { "UserID", "ItemID" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
