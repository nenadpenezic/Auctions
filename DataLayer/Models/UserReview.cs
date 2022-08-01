using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class UserReview
    {
        public int UserReviewID { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public int ReviewerID { get; set; }
        public User Reviewer { get; set; }
        public int Grade { get; set; }
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; }

    }
}
