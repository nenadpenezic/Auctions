using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionsAppAPI.DTO
{
    public class UserAdministrationProfile
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string EmailForContact { get; set; }
        public int PhoneNumber { get; set;}
        public bool IsBlocked { get; set; }


    }
}
