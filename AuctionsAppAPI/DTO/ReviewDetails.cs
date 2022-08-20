using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionsAppAPI.DTO
{
    public class ReviewDetails
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public int Grade { get; set; }
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
