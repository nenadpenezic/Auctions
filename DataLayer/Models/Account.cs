﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Account
    {
        public int AccountID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string VerificationString { get; set; }
        public bool IsAccountVerifyed { get; set; }

    }
}
