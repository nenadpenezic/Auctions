using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionsAppAPI.DTO
{
    public class AdministratorItemDetails
    {
        public int ItemID { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string ItemName { get; set; }

        
        public double Price { get; set; }
        public bool IsItemBlocked { get; set; }
        public DateTime AddedDate { get; set; }
        public bool IsSold { get; set; }

    }
}
