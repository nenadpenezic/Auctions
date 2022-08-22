using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionsAppAPI.DTO
{
    public class LogIn
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [MinLength(10)]
        public string Password { get; set; }
    }
}
