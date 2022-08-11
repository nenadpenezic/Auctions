using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionsAppAPI.DTO
{
    public class NewItemSpecification
    {
        public string SpecificationName { get; set; }
        public string SpecificationValue { get; set; }
    }

    public class NewItemSpecifications
    {
        public ICollection <NewItemSpecification> NewItemSpecificationList { get; set; }
    }
}
