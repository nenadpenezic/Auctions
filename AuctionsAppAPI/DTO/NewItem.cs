using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionsAppAPI.DTO
{
    public class NewItem
    {
        public string ItemName { get; set; }
        public string Description { get; set; }
        public string NewItemSpecifications { get; set; }
        public int CategoryID { get; set; }
        public IEnumerable<IFormFile> ItemPictures {get; set;}
    }
}
