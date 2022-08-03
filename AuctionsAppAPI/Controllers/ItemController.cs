using AuctionsAppAPI.Authorization;
using AuctionsAppAPI.DTO;
using DataLayer.DatabaseConfiguration;
using DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionsAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private AuctionsDBContext auctionsDBContext;
        private ITokenAuthorization tokenAuthorization;
        public ItemController(
            AuctionsDBContext _auctionsDBContext,
            ITokenAuthorization _tokenAuthorization)
        {
            auctionsDBContext = _auctionsDBContext;
            tokenAuthorization = _tokenAuthorization;
        }
        [HttpPost("add-item")]
        public ActionResult AddItem([FromBody] NewItem newItem)
        {
            Item item = new Item()
            {
                ItemName = newItem.ItemName,
                OwnerID = tokenAuthorization.GetCurrentUser(User.Claims),
                Description = newItem.Description,
                AddedDate = DateTime.Now,
                SoldDate = DateTime.Now
            };

            ICollection<ItemSpecification> icl = new List<ItemSpecification>();

            foreach (NewItemSpecification element in newItem.NewItemSpecifications)
            {
                ItemSpecification itemSpecification = new ItemSpecification()
                {
                    SpecificationName = element.SpecificationName,
                    SpecificationValue = element.SpecificationValue
                };

                icl.Add(itemSpecification);
            }
            item.ItemSpecifications = icl;

            auctionsDBContext.Items.Add(item);
            auctionsDBContext.SaveChanges();

            return Ok();
        }
    }
}
