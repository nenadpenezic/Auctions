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
        public int PhoneNumber { get; set; }
        public IFormFile ProfilePicture { get; set; }
    }
}
