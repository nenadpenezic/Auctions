using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Offer
    {   [Key]
        public int OfferID { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int ItemID { get; set; }
        public Item Item { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int UserID { get; set; }
        public User User { get; set; }
        [Required]
        [Column(TypeName = "datetime2(7)")]
        public DateTime OfferDate { get; set; }
        
        [Required]
        public double Value { get; set; }
        [Required]
        public bool isAccepted { get; set; }


    }
}
