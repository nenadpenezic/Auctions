using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class ItemAuctionParticipant
    {
        public int UserID { get; set; }
        public User User { get; set; }
        public Item Item { get; set; }
        public int ItemID { get; set; }
        public ICollection<Offer> Offers { get; set; }

    }
}
