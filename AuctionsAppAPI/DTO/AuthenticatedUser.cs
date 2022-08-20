using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionsAppAPI.DTO
{
    public class AuthenticatedUser
    {
        public int UserID { get; set; }
        public bool IsAccountComplete { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public ICollection<NotificationDetails> Notifications {get; set;}
        public string ProfilePhoto { get; set; }
        public string Role { get; set; }
    }
}
