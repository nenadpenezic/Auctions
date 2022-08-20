using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionsAppAPI.DTO
{
    public class OfferDetails
    {
        public int OfferID { get; set; }
        public int ItemID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public DateTime AddedDate { get; set; }
        public string Currency { get; set; }
        public double Value { get; set; }
        public bool IsAccepted { get; set; }
    }
}
