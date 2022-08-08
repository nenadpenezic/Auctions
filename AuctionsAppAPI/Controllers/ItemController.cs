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
                CategoryID = newItem.CategoryID,
                Price = 500
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

            auctionsDBContext.SaveChanges();

            return Ok();
        }
        [HttpGet("get-user-items/{userID}")]
        public ActionResult GetUserItems(int userID)
        {
            List<Item> userItems = auctionsDBContext.Items.Where(item => item.OwnerID == userID).ToList();
            List<UserItem> userItemsDTO = new List<UserItem>();

            foreach (Item item in userItems)
            {
                bool acceptedOffer = item.AcceptedOffer==null?false:true;
                userItemsDTO.Add(new UserItem()
                {   ItemID = item.ItemID,
                    ItemName = item.ItemName,
                    CurrentPrice = item.Price,
                    IsSold = acceptedOffer

                });
            }

            return Ok(userItemsDTO);
        }

        [HttpGet("item-details/{itemID}")]
        public ActionResult GetItemDetails(int itemID)
        {
            ItemDetails itemDetails = auctionsDBContext.Items.Where(item => item.ItemID == itemID)
                .Select(item => new ItemDetails
                {
                    ItemID = item.ItemID,
                    OwnerID = item.OwnerID,
                    ItemName = item.ItemName,
                    Description = item.Description,
                    ItemSpecifications = item.ItemSpecifications,
                    Offers = item.Offers.OrderByDescending(order=>order.Value)
                    .Select(offer=> new ItemDetailsOffer{

                        OfferID = offer.OfferID,
                        UserID = offer.UserID,
                        Name = offer.User.Name,
                        Lastname = offer.User.Lastname,
                        OfferDate = offer.OfferDate,
                        Value = offer.Value,
                        IsAccepted = offer.isAccepted
                    
                    }).ToList(),
                    AddedDate = item.AddedDate,
                    SoldDate = item.SoldDate,
                    Price = item.Price
                }).FirstOrDefault();

            return Ok(itemDetails);
        }
    }
}
