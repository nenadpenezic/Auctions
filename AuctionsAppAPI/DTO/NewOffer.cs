using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionsAppAPI.DTO
{
    public class NewOffer
    {
        [Required]
        public int ItemID { get; set; }
        [Required]
        public double Value { get; set; }
    }
}
