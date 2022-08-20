using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public Account Account { get; set; }
        [Column(TypeName = "varchar(20)")]
        [Required]
        public string Name { get; set; }
        [Column(TypeName = "varchar(20)")]
        [Required]
        public string Lastname { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string? ProfilePhoto { get; set; }
        [Column(TypeName = "varchar(35)")]
        [Required]
        public string EmailForContact { get; set; }
        [Column(TypeName = "varchar(15)")]  //datetime2(7)
        [Required]
        public string PhoneNumber { get; set; }
        [Column(TypeName = "varchar(25)")]
        [Required]
        public string Location { get; set; }
        [Column(TypeName = "datetime2(7)")]
        [Required]
        public DateTime JoinDate { get; set; }
        [Column(TypeName = "datetime2(7)")] 
        [Required]

        public DateTime? LastTimeOnline { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection <Review> UserPersonalReviews { get; set; }
        public ICollection <Item> Items { get; set; }

    }
}
