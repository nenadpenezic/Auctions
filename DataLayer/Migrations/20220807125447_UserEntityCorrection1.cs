using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class UserEntityCorrection1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuctionParticipants_Items_ItemID",
                table: "AuctionParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_AuctionParticipants_Users_UserID",
                table: "AuctionParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemPhotos_Items_ItemID",
                table: "ItemPhotos");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Categorys_CategoryID",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Users_OwnerID",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemSpecifications_Items_ItemID",
                table: "ItemSpecifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Users_UserID",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_AuctionParticipants_UserID_ItemID",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserReviews_Users_ReviewerID",
                table: "UserReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_UserReviews_Users_UserID",
                table: "UserReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Accounts_UserID",
                table: "Users");

            migrationBuilder.AddForeignKey(
                name: "FK_AuctionParticipants_Items_ItemID",
                table: "AuctionParticipants",
                column: "ItemID",
                principalTable: "Items",
                principalColumn: "ItemID");

            migrationBuilder.AddForeignKey(
                name: "FK_AuctionParticipants_Users_UserID",
                table: "AuctionParticipants",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPhotos_Items_ItemID",
                table: "ItemPhotos",
                column: "ItemID",
                principalTable: "Items",
                principalColumn: "ItemID");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Categorys_CategoryID",
                table: "Items",
                column: "CategoryID",
                principalTable: "Categorys",
                principalColumn: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Users_OwnerID",
                table: "Items",
                column: "OwnerID",
                principalTable: "Users",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemSpecifications_Items_ItemID",
                table: "ItemSpecifications",
                column: "ItemID",
                principalTable: "Items",
                principalColumn: "ItemID");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Users_UserID",
                table: "Notifications",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_AuctionParticipants_UserID_ItemID",
                table: "Offers",
                columns: new[] { "UserID", "ItemID" },
                principalTable: "AuctionParticipants",
                principalColumns: new[] { "UserID", "ItemID" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserReviews_Users_ReviewerID",
                table: "UserReviews",
                column: "ReviewerID",
                principalTable: "Users",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_UserReviews_Users_UserID",
                table: "UserReviews",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Accounts_UserID",
                table: "Users",
                column: "UserID",
                principalTable: "Accounts",
                principalColumn: "AccountID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuctionParticipants_Items_ItemID",
                table: "AuctionParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_AuctionParticipants_Users_UserID",
                table: "AuctionParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemPhotos_Items_ItemID",
                table: "ItemPhotos");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Categorys_CategoryID",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Users_OwnerID",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemSpecifications_Items_ItemID",
                table: "ItemSpecifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Users_UserID",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Offers_AuctionParticipants_UserID_ItemID",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserReviews_Users_ReviewerID",
                table: "UserReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_UserReviews_Users_UserID",
                table: "UserReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Accounts_UserID",
                table: "Users");

            migrationBuilder.AddForeignKey(
                name: "FK_AuctionParticipants_Items_ItemID",
                table: "AuctionParticipants",
                column: "ItemID",
                principalTable: "Items",
                principalColumn: "ItemID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AuctionParticipants_Users_UserID",
                table: "AuctionParticipants",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPhotos_Items_ItemID",
                table: "ItemPhotos",
                column: "ItemID",
                principalTable: "Items",
                principalColumn: "ItemID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Categorys_CategoryID",
                table: "Items",
                column: "CategoryID",
                principalTable: "Categorys",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Users_OwnerID",
                table: "Items",
                column: "OwnerID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemSpecifications_Items_ItemID",
                table: "ItemSpecifications",
                column: "ItemID",
                principalTable: "Items",
                principalColumn: "ItemID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Users_UserID",
                table: "Notifications",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_AuctionParticipants_UserID_ItemID",
                table: "Offers",
                columns: new[] { "UserID", "ItemID" },
                principalTable: "AuctionParticipants",
                principalColumns: new[] { "UserID", "ItemID" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserReviews_Users_ReviewerID",
                table: "UserReviews",
                column: "ReviewerID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserReviews_Users_UserID",
                table: "UserReviews",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Accounts_UserID",
                table: "Users",
                column: "UserID",
                principalTable: "Accounts",
                principalColumn: "AccountID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
