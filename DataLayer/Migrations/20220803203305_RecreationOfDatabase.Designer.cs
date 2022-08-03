﻿// <auto-generated />
using System;
using DataLayer.DatabaseConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataLayer.Migrations
{
    [DbContext(typeof(AuctionsDBContext))]
    [Migration("20220803203305_RecreationOfDatabase")]
    partial class RecreationOfDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataLayer.Models.Account", b =>
                {
                    b.Property<int>("AccountID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAccountVerifyed")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VerificationString")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AccountID");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("DataLayer.Models.Item", b =>
                {
                    b.Property<int>("ItemID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AddedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ItemName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OwnerUserID")
                        .HasColumnType("int");

                    b.Property<DateTime>("SoldDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ItemID");

                    b.HasIndex("OwnerUserID");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("DataLayer.Models.ItemAuctionParticipant", b =>
                {
                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int>("ItemID")
                        .HasColumnType("int");

                    b.HasKey("UserID", "ItemID");

                    b.HasIndex("ItemID");

                    b.ToTable("AuctionParticipants");
                });

            modelBuilder.Entity("DataLayer.Models.ItemSpecification", b =>
                {
                    b.Property<int>("ItemSpecificationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ItemID")
                        .HasColumnType("int");

                    b.Property<string>("SpecificationName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SpecificationValue")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ItemSpecificationID");

                    b.HasIndex("ItemID");

                    b.ToTable("ItemSpecifications");
                });

            modelBuilder.Entity("DataLayer.Models.Notification", b =>
                {
                    b.Property<int>("NotificationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ArriveDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("NotificationText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Open")
                        .HasColumnType("bit");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("NotificationID");

                    b.HasIndex("UserID");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("DataLayer.Models.Offer", b =>
                {
                    b.Property<int>("OfferID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AuctionParticipantItemID")
                        .HasColumnType("int");

                    b.Property<int?>("AuctionParticipantUserID")
                        .HasColumnType("int");

                    b.Property<DateTime>("OfferDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("OfferID");

                    b.HasIndex("AuctionParticipantUserID", "AuctionParticipantItemID");

                    b.ToTable("Offers");
                });

            modelBuilder.Entity("DataLayer.Models.User", b =>
                {
                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int>("JMBG")
                        .HasColumnType("int");

                    b.Property<DateTime>("JoinDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastTimeOnline")
                        .HasColumnType("datetime2");

                    b.Property<string>("Lastname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("int");

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DataLayer.Models.UserReview", b =>
                {
                    b.Property<int>("UserReviewID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Grade")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReviewDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ReviewerID")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("UserReviewID");

                    b.HasIndex("ReviewerID");

                    b.HasIndex("UserID");

                    b.ToTable("UserReviews");
                });

            modelBuilder.Entity("DataLayer.Models.Item", b =>
                {
                    b.HasOne("DataLayer.Models.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerUserID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("DataLayer.Models.ItemAuctionParticipant", b =>
                {
                    b.HasOne("DataLayer.Models.Item", "Item")
                        .WithMany("ItemAuctionParticipants")
                        .HasForeignKey("ItemID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DataLayer.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataLayer.Models.ItemSpecification", b =>
                {
                    b.HasOne("DataLayer.Models.Item", "Item")
                        .WithMany("ItemSpecifications")
                        .HasForeignKey("ItemID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Item");
                });

            modelBuilder.Entity("DataLayer.Models.Notification", b =>
                {
                    b.HasOne("DataLayer.Models.User", "User")
                        .WithMany("Notifications")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataLayer.Models.Offer", b =>
                {
                    b.HasOne("DataLayer.Models.ItemAuctionParticipant", "AuctionParticipant")
                        .WithMany("Offers")
                        .HasForeignKey("AuctionParticipantUserID", "AuctionParticipantItemID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("AuctionParticipant");
                });

            modelBuilder.Entity("DataLayer.Models.User", b =>
                {
                    b.HasOne("DataLayer.Models.Account", "Account")
                        .WithOne()
                        .HasForeignKey("DataLayer.Models.User", "UserID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("DataLayer.Models.UserReview", b =>
                {
                    b.HasOne("DataLayer.Models.User", "Reviewer")
                        .WithMany()
                        .HasForeignKey("ReviewerID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("DataLayer.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Reviewer");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataLayer.Models.Item", b =>
                {
                    b.Navigation("ItemAuctionParticipants");

                    b.Navigation("ItemSpecifications");
                });

            modelBuilder.Entity("DataLayer.Models.ItemAuctionParticipant", b =>
                {
                    b.Navigation("Offers");
                });

            modelBuilder.Entity("DataLayer.Models.User", b =>
                {
                    b.Navigation("Notifications");
                });
#pragma warning restore 612, 618
        }
    }
}
