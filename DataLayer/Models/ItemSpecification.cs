using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class ItemSpecification
    {
        public string ItemSpecificationID { get; set; }
        public Item Item { get; set; }
        public string SpecificationName { get; set; }
        public string SpecificationValue { get; set; }
    }
}
