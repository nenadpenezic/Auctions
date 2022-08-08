using AuctionsAppAPI.Authorization;
using AuctionsAppAPI.DTO;
using DataLayer.DatabaseConfiguration;
using DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public ActionResult AddItem([FromBody] NewItem newItem)
        {
            Item item = new Item()
            {
                ItemName = newItem.ItemName,
                OwnerID = tokenAuthorization.GetCurrentUser(User.Claims),
                Description = newItem.Description,
                AddedDate = DateTime.Now,
                CategoryID =newItem.CategoryID
            };

            ICollection<ItemSpecification> itemSpecifications = new List<ItemSpecification>();

            foreach (NewItemSpecification element in newItem.NewItemSpecifications)
                itemSpecifications.Add(new ItemSpecification()
                {
                    SpecificationName = element.SpecificationName,
                    SpecificationValue = element.SpecificationValue
                });
            
            item.ItemSpecifications = itemSpecifications;

            auctionsDBContext.Items.Add(item);

            if (auctionsDBContext.SaveChanges() <= 0)
                return StatusCode(500);

            return Ok("Item inserted.");
        }
        [HttpGet("get-user-items/{userID}")]
        public ActionResult GetUserItems(int userID)
        {
            List<UserItem> userItems = auctionsDBContext.Items.Where(item => item.OwnerID == userID)
                .Select(item => new UserItem
                {
                    ItemName = item.ItemName,
                    CurrentPrice = 0,
                }).ToList();

            return Ok(userItems);
        }
    }
}
