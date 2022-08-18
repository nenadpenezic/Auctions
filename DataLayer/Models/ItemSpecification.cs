using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class ItemSpecification
    {   [Key]
        public int ItemSpecificationID { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int ItemID { get; set; }
        public Item Item { get; set; }
        [Required]
        [Column(TypeName = "varchar(15)")]
        public string SpecificationName { get; set; }
        [Required]
        [Column(TypeName = "varchar(25)")]
        public string SpecificationValue { get; set; }
    }
}
