using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionsAppAPI.DTO
{
    public class NewUser
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string EmailForContact { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        public IFormFile ProfilePicture { get; set; }
    }
}
