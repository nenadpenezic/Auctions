using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class UserReview
    {   [Key]
        public int UserReviewID { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int UserID { get; set; }
        public User User { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int ReviewerID { get; set; }
        public User Reviewer { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int Grade { get; set; }
        [Required]
        [Column(TypeName = "varchar(60)")]
        public string Comment { get; set; }
        [Required]
        [Column(TypeName = "datetime2(7)")]
        public DateTime ReviewDate { get; set; }

    }
}
