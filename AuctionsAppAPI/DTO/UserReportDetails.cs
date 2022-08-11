using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionsAppAPI.DTO
{
    public class UserReportDetails
    {
        public string ReporterName { get; set; }
        public string ReporterLastname { get; set; }

        public string ReportAgainstName { get; set; }
        public string ReportAgainsLastname { get; set; }


        public string ReportTitle { get; set; }
        public string ReportDetails { get; set; }
        public DateTime DateTime { get; set; }
    }
}
