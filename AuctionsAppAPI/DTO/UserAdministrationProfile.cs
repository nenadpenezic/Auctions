using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionsAppAPI.DTO
{
    public class UserAdministrationProfile
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string EmailForContact { get; set; }
        public string PhoneNumber { get; set;}
        public bool IsBlocked { get; set; }


    }
}
