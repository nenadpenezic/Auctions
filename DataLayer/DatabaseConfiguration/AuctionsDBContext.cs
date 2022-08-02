using DataLayer.Models;
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
            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<User>()
                .HasOne(user =>user.Account )
                .WithOne()
                .HasForeignKey<User>(acc=>acc.UserID);


            modelBuilder.Entity<UserReview>()
                .HasKey(ur => ur.UserReviewID);

            modelBuilder.Entity<UserReview>()
                .HasOne(user => user.User)
                .WithMany()
                .HasForeignKey(ur => ur.UserID);

            modelBuilder.Entity<UserReview>()
               .HasOne(user => user.Reviewer)
               .WithMany()
               .HasForeignKey(ur => ur.ReviewerID);

            modelBuilder.Entity<AuctionParticipant>()
                .HasKey(ap => new { ap.UserID, ap.AuctionID });

        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserReview> UserReviews { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemSpecification> ItemSpecifications { get; set; }
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<AuctionParticipant> AuctionParticipants { get; set; }
        public DbSet<Offer> Offers { get; set; }

    }
}
