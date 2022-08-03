using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class ForeignKeysCorrectionInOfferTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_AuctionParticipants_AuctionParticipantUserID_AuctionParticipantItemID",
                table: "Offers");

            migrationBuilder.RenameColumn(
                name: "AuctionParticipantUserID",
                table: "Offers",
                newName: "ItemAuctionParticipantUserID");

            migrationBuilder.RenameColumn(
                name: "AuctionParticipantItemID",
                table: "Offers",
                newName: "ItemAuctionParticipantItemID");

            migrationBuilder.RenameIndex(
                name: "IX_Offers_AuctionParticipantUserID_AuctionParticipantItemID",
                table: "Offers",
                newName: "IX_Offers_ItemAuctionParticipantUserID_ItemAuctionParticipantItemID");

            migrationBuilder.AddColumn<int>(
                name: "ItemID",
                table: "Offers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Offers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Offers_ItemID_UserID",
                table: "Offers",
                columns: new[] { "ItemID", "UserID" });

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_AuctionParticipants_ItemAuctionParticipantUserID_ItemAuctionParticipantItemID",
                table: "Offers",
                columns: new[] { "ItemAuctionParticipantUserID", "ItemAuctionParticipantItemID" },
                principalTable: "AuctionParticipants",
                principalColumns: new[] { "UserID", "ItemID" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_AuctionParticipants_ItemID_UserID",
                table: "Offers",
                columns: new[] { "ItemID", "UserID" },
                principalTable: "AuctionParticipants",
                principalColumns: new[] { "UserID", "ItemID" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_AuctionParticipants_ItemAuctionParticipantUserID_ItemAuctionParticipantItemID",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_AuctionParticipants_ItemID_UserID",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_Offers_ItemID_UserID",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "ItemID",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Offers");

            migrationBuilder.RenameColumn(
                name: "ItemAuctionParticipantUserID",
                table: "Offers",
                newName: "AuctionParticipantUserID");

            migrationBuilder.RenameColumn(
                name: "ItemAuctionParticipantItemID",
                table: "Offers",
                newName: "AuctionParticipantItemID");

            migrationBuilder.RenameIndex(
                name: "IX_Offers_ItemAuctionParticipantUserID_ItemAuctionParticipantItemID",
                table: "Offers",
                newName: "IX_Offers_AuctionParticipantUserID_AuctionParticipantItemID");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_AuctionParticipants_AuctionParticipantUserID_AuctionParticipantItemID",
                table: "Offers",
                columns: new[] { "AuctionParticipantUserID", "AuctionParticipantItemID" },
                principalTable: "AuctionParticipants",
                principalColumns: new[] { "UserID", "ItemID" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
