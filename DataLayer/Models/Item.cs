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
    {   [Key]
        public int ItemID { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int OwnerID { get; set; }
        public User Owner { get; set; }
        [Required]
        [Column(TypeName = "varchar(35)")]
        public string ItemName { get; set; }
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Description { get; set; }
        public ICollection<ItemSpecification> ItemSpecifications { get; set; }
        public ICollection<Offer> Offers { get; set; }
        public int? AcceptedOfferID { get; set; }
        public Offer? AcceptedOffer { get; set; }
        [Column(TypeName = "datetime2(7)")]  //datetime2(7)
        [Required]
        public DateTime AddedDate { get; set; }
        [Column(TypeName = "datetime2(7)")]  //datetime2(7)
        [Required]
        public DateTime? SoldDate { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int CategoryID { get; set; }
        public Category Category { get; set; }
        public ICollection<ItemPhoto> ItemPhotos { get; set; }
        [Required]
        public double Price { get; set; }
        public bool IsItemBlocked { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int CurrencyID { get; set; }
        public Currency Currency { get; set; }

    }
}
