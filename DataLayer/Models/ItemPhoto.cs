using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class ItemPhoto
    {   [Key]
        public int ItemPhotoID { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int ItemID { get; set; }
        public Item Item { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string PhotoUrl { get; set; }
    }
}
