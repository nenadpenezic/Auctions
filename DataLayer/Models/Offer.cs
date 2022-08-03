using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Offer
    {
        public int OfferID { get; set; }
        public int UserID { get; set; }
        public int ItemID { get; set; }
        public DateTime OfferDate { get; set; }
        public ItemAuctionParticipant ItemAuctionParticipant { get; set;}
        public int Value { get; set; }


    }
}
