using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionsAppAPI.DTO
{
    public class ItemDetails
    {


        public int ItemID { get; set; }
        public int OwnerID { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public ICollection<ItemSpecification> ItemSpecifications { get; set; }
        public ICollection<OfferDetails> Offers { get; set; }
        public ICollection<string> ItemPhotos { get; set; }
        public DateTime AddedDate { get; set; }
        
        public int CategoryID { get; set; }
     
        public double CurrentPrice { get; set; }
    }
}
