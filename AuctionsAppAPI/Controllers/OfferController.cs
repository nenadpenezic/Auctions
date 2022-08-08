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
    public class OfferController : ControllerBase
    {
        private AuctionsDBContext auctionsDBContext;
        private ITokenAuthorization tokenAuthorization;
        public OfferController(
            AuctionsDBContext _auctionsDBContext,
            ITokenAuthorization _tokenAuthorization)
        {
            auctionsDBContext = _auctionsDBContext;
            tokenAuthorization = _tokenAuthorization;
        }

        [HttpPost("add-offer/{itemID}")]
        [Authorize]
        public ActionResult AddOffer(int itemID,[FromBody] int offerValue)
        {
            int UserID = tokenAuthorization.GetCurrentUser(User.Claims);

            Offer lastOffer = auctionsDBContext.Offers
                .OrderByDescending(offer => offer.Value)
                .Where(offer=>offer.ItemID == itemID)
                .FirstOrDefault();

            if(lastOffer != null)
            {
                if (lastOffer.isAccepted)
                    return BadRequest("Item is aready sold!");

                if (lastOffer.Value > offerValue)
                    return BadRequest("You must offer higher sum than previus one!");
            }


            Offer offer = new Offer()
            {
               ItemID = itemID,
               UserID = UserID,
               Value = offerValue,
               OfferDate = DateTime.Now
            };
            

            auctionsDBContext.Offers.Add(offer);

            Item offerItem = auctionsDBContext.Items.Find(itemID);
            offerItem.Price = offerValue;

            if (auctionsDBContext.SaveChanges()>0)
                return Ok();

            return StatusCode(500);

        }
        [HttpGet("item-offers/{itemID}")]
        public ActionResult GetProductOffers(int itemID)
        {
            List<ItemDetailsOffer> offers = auctionsDBContext.Offers.OrderByDescending(order => order.Value)
                    .Select(offer => new ItemDetailsOffer
                    {
                        OfferID = offer.OfferID,
                        UserID = offer.UserID,
                        Name = offer.User.Name,
                        Lastname = offer.User.Lastname,
                        OfferDate = offer.OfferDate,
                        Value = offer.Value,
                        IsAccepted = offer.isAccepted

                    }).ToList();
            return Ok(offers);
        }

        [HttpPost("accept-offer/{offerID}")]
        public ActionResult AcceptOffer(int offerID) 
        {
            Offer offer = auctionsDBContext.Offers.Where(offer=>offer.OfferID == offerID).FirstOrDefault();
            offer.isAccepted = true;
            Item item = auctionsDBContext.Items.Where(item => item.ItemID == offer.ItemID).FirstOrDefault();
            item.AcceptedOfferID = offer.ItemID;
            auctionsDBContext.SaveChanges();

            return Ok();
        }
    }
}
