using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class AuctionParticipant
    {
        public int UserID { get; set; }
        public User User { get; set; }
        public int AuctionID { get; set; }
        public Auction Auction { get; set; }
        public ICollection<Offer> Offers { get; set; }

    }
}
