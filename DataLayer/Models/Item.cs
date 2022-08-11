using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Item
    {
        public int ItemID { get; set; }
        [Required]
        public int OwnerID { get; set; }
        public User Owner { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string ItemName { get; set; }
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Description { get; set; }
        public ICollection<ItemSpecification> ItemSpecifications { get; set; }

        public ICollection<Offer> Offers { get; set; }

        public int? AcceptedOfferID { get; set; }
        public Offer? AcceptedOffer { get; set; }
        [Required]
        public DateTime AddedDate { get; set; }
        public DateTime? SoldDate { get; set; }
        [Required]
        public int CategoryID { get; set; }
        public Category Category { get; set; }
        public ICollection<ItemPhoto> ItemPhotos { get; set; }
        public double Price { get; set; }
        public bool IsItemBlocked { get; set; }

    }
}
