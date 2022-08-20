using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionsAppAPI.DTO
{
    public class NewUser
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string EmailForContact { get; set; }
        public string PhoneNumber { get; set; }
        public string Location { get; set; }
        public DateTime DateOfBirth { get; set; }
        public IFormFile ProfilePicture { get; set; }
    }
}
