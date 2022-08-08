using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionsAppAPI.DTO
{
    public class UserItem
    {
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public double CurrentPrice { get; set; }
        public bool IsSold { get; set; }
    }
}
