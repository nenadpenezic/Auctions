using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionsAppAPI.DTO
{
    public class NewItem
    {
        [Required]
        public string ItemName { get; set; }
        [Required]
        public string Description { get; set; }
        public string NewItemSpecifications { get; set; }
        [Required]
        public int CategoryID { get; set; }
        [Required]
        public int CurrencyID { get; set; }
        [Required]
        public double Price { get; set; }
        public IEnumerable<IFormFile> ItemPictures {get; set;}
    }
}
