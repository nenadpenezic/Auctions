using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionsAppAPI.DTO
{
    public class ItemPreview
    {
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public double CurrentPrice { get; set; }
        public string Category { get; set; }
        public bool IsSold { get; set; }
        public string PreviewImage { get; set; }
    }
}
