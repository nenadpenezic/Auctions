using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class UserReport
    {   [Key]
        public int UserReportID { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int UserReporterID { get; set; }
        public User UserReporter { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int ReportAgainstUserID { get; set; }
        public User ReportAgainstUser { get; set; }
        [Required]
        [Column(TypeName = "varchar(15)")]
        public string ReportTitle { get; set; }
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string ReportDetails { get; set; }
        [Required]
        [Column(TypeName = "datetime2(7)")]
        public DateTime DateTime { get; set; }
    }
}
