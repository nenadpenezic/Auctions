﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionsAppAPI.DTO
{
    public class AuthenticatedUser
    {
        public int UserID { get; set; }
        public bool IsAccountComplete { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
    }
}