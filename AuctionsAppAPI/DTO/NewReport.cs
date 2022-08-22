using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionsAppAPI.DTO
{
    public class NewReport
    {
        [Required]
        public int UserID { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Explanation { get; set; }
    }
}
