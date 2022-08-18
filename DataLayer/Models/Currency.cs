using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Currency
    {   [Key]
        public int CurrencyID { get; set; }
        [Column(TypeName = "varchar(10)")]
        [Required]
        public string CurrencyName { get; set; }

    }
}
