using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Auction
    {
        public int AuctionID { get; set; }
        public User User { get; set; }
        public int UserID { get; set; }
        public int ItemID { get; set; }
        public Item Item { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime AuctionStartDate { get; set; }
        public bool Active { get; set; }
        public ICollection<ItemAuctionParticipant> AuctionParticipants { get; set; }


    }
}
