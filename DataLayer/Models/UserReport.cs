using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class UserReport
    {
        public int UserReportID { get; set; }
        public int UserReporterID { get; set; }
        public User UserReporter { get; set; }
        public int ReportAgainstUserID { get; set; }
        public User ReportAgainstUser { get; set; }
        public string ReportTitle { get; set; }
        public string ReportDetails { get; set; }
        public DateTime DateTime { get; set; }
    }
}
