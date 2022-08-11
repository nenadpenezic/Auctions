﻿using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DatabaseConfiguration
{
    public class AuctionsDBContext:DbContext
    {
        public AuctionsDBContext(DbContextOptions dbContextOptions):base(dbContextOptions)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           

            

            modelBuilder.Entity<User>()
                .HasOne(user => user.Account )
                .WithOne()
                .HasForeignKey<User>(user=>user.UserID);


            modelBuilder.Entity<UserReview>()
                .HasKey(ur => ur.UserReviewID);

            modelBuilder.Entity<UserReview>()
                .HasOne(user => user.User)
                .WithMany(user=>user.UserPersonalReviews)
                .HasForeignKey(ur => ur.UserID);

            modelBuilder.Entity<UserReview>()
               .HasOne(user => user.Reviewer)
               .WithMany()
               .HasForeignKey(ur => ur.ReviewerID);

            modelBuilder.Entity<Item>()
                .HasOne(owner => owner.Owner)
                .WithMany(user=>user.Items)
                .HasForeignKey(owner => owner.OwnerID);

            modelBuilder.Entity<Offer>()
                .HasOne(offer => offer.Item)
                .WithMany(item => item.Offers)
                .HasForeignKey(offer => offer.ItemID);

            modelBuilder.Entity<Item>()
                .HasOne(item => item.AcceptedOffer)
                .WithMany()
                .HasForeignKey(item => item.AcceptedOfferID);

            modelBuilder.Entity<UserReport>()
                .HasOne(ur => ur.UserReporter)
                .WithMany()
                .HasForeignKey(ur => ur.UserReporterID);

            modelBuilder.Entity<UserReport>()
                .HasOne(ur => ur.ReportAgainstUser)
                .WithMany()
                .HasForeignKey(ur => ur.ReportAgainstUserID);


            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.NoAction;
            }
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserReview> UserReviews { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemSpecification> ItemSpecifications { get; set; }
        //public DbSet<Auction> Auctions { get; set; }
        //public DbSet<ItemAuctionParticipant> AuctionParticipants { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<ItemPhoto> ItemPhotos { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<UserReport> UserReports { get; set; }
    }
}
