using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionsAppAPI.DTO
{
    public class ItemSearchQuery
    {
        public string ItemName { get; set; }
        //public int CategoryID { get; set; }
        public double StartPrice { get; set; }
        public double EndPrice { get; set; }
        public bool IsSold { get; set; }
    }
}
