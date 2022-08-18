using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Account
    {   [Key]
        public int AccountID { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Email { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Password { get; set; }
        [Required]
        [Column(TypeName = "varchar(210)")]
        public string VerificationString { get; set; }
        [Required]
        public bool IsAccountVerifyed { get; set; }
        [Required]
        public bool IsBlocked { get; set; }
        public int RoleID { get; set; }
        public Role Role { get; set; }
    }
}
