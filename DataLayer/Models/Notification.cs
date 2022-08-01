using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Notification
    {
        public int NotificationID { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public string NotificationText { get; set; }
        public DateTime ArriveDate { get; set; }
        public bool Open { get; set; }

    }
}
