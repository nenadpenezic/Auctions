using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionsAppAPI.DTO
{
    public class NewReview
    {
        [Required]
        public int Grade { get; set; }
        public string Comment { get; set; }
    }
}
