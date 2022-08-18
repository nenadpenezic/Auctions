using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Notification
    {   [Key]
        public int NotificationID { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int UserID { get; set; }
        public User User { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string NotificationText { get; set; }
        [Required]
        [Column(TypeName = "datetime2(7)")]
        public DateTime ArriveDate { get; set; }
        [Required]
        public bool Open { get; set; }

    }
}
