﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionsAppAPI.DTO
{
    public class ItemDetailsOffer
    {
        public int OfferID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public DateTime OfferDate { get; set; }
        public int Value { get; set; }
        public bool IsAccepted { get; set; }
    }
}
