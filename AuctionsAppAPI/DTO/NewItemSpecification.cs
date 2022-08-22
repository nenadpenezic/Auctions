using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionsAppAPI.DTO
{
    public class NewItemSpecification
    {
        [Required]
        public string SpecificationName { get; set; }
        [Required]
        public string SpecificationValue { get; set; }
    }

    public class NewItemSpecifications
    {
        public ICollection <NewItemSpecification> NewItemSpecificationList { get; set; }
    }
}
