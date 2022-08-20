using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionsAppAPI.DTO
{
    public class NotificationDetails
    {
        public int NotificationID { get; set; }
        public string NotificationText { get; set; }
        public DateTime ArriveDate { get; set; }
        public bool Open { get; set; }
    }
}
