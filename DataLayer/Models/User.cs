using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class User
    {
     
        public int UserID { get; set; }
        public Account Account { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string ProfilePhoto { get; set; }
        public string EmailForContact { get; set; }
        public int PhoneNumber { get; set; }
        public DateTime JoinDate { get; set; }
        public DateTime LastTimeOnline { get; set; }
        public ICollection<Notification> Notifications { get; set; }

        public ICollection <UserReview> UserPersonalReviews { get; set; }

        public ICollection <Item> Items { get; set; }

    }
}
