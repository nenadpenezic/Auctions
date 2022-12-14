using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Category
    {   [Key]
        public int CategoryID { get; set; }
        [Column(TypeName = "varchar(50)")]
        [Required]
        public string CategoryName { get; set; }
    }
}
