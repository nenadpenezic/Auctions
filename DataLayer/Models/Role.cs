using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Role
    {   [Key]
        public int RoleID { get; set; }
        [Column(TypeName = "varchar(15)")]
        [Required]
        public string RoldeName { get; set; }
    }
}
