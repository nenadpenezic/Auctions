using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionsAppAPI.DTO
{
    public class OfferView
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public DateTime AddedDate { get; set; }
        public double Value { get; set; }
    }
}
