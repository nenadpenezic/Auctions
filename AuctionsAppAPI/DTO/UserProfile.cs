using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionsAppAPI.DTO
{
    public class UserProfile
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string ProfilePhoto { get; set; }
        public string EmailForContact { get; set; }
        public int PhoneNumber { get; set; }
        public DateTime JoinDate { get; set; }
        public DateTime LastTimeOnline { get; set; }

        public double AverageGrade { get; set; }

        public int NumberOfReviews { get; set; }

        public int NumberOfItemsOnSale { get; set; }
    }
}
