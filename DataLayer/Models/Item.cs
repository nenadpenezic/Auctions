using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Item
    {
        public int ItemID { get; set; }
        public int OwnerID { get; set; }
        public User Owner { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public ICollection<ItemSpecification> ItemSpecifications { get; set; }
        public ICollection<ItemAuctionParticipant> ItemAuctionParticipants { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime SoldDate { get; set; }


    }
}
