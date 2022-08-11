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
        public ActionResult AddOffer([FromBody] NewOffer offerValue,int itemID)
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

                if (lastOffer.Value > offerValue.Value)
                    return BadRequest("You must offer higher sum than previus one!");
            }


            Offer offer = new Offer()
            {
               ItemID = itemID,
               UserID = UserID,
               Value = offerValue.Value,
               OfferDate = DateTime.Now
            };
            

            auctionsDBContext.Offers.Add(offer);

            Item offerItem = auctionsDBContext.Items.Find(itemID);
            offerItem.Price = offerValue.Value;

            auctionsDBContext.SaveChanges();

            ItemDetailsOffer itemDetailsOffer = auctionsDBContext.Offers
                .Where(off => off.OfferID == offer.OfferID)
                .Select(offer => new ItemDetailsOffer()
            {
                OfferID = offer.OfferID,
                Name = offer.User.Name,
                Lastname = offer.User.Lastname,
                Value = offer.Value,
                OfferDate = offer.OfferDate,
                IsAccepted = offer.isAccepted
            }).FirstOrDefault();

            if(lastOffer==null)
                return Ok(itemDetailsOffer);

            Notification notification = new Notification()
            {
                UserID = lastOffer.UserID,
                NotificationText = "Your offer for " + offerItem.ItemName + " is not longer highest!",
                ArriveDate = DateTime.Now,
                Open = false
            };
            auctionsDBContext.Notifications.Add(notification);
            auctionsDBContext.SaveChanges();

            return Ok(itemDetailsOffer);

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

        [HttpGet("accept-offer/{offerID}")]
        [Authorize]
        public ActionResult AcceptOffer(int offerID) 
        {
             Offer offer = auctionsDBContext.Offers
                .Where(offer=>offer.OfferID == offerID)
                .FirstOrDefault();

             if (offer.isAccepted)
                return BadRequest("Offer accepted aready");
             offer.isAccepted = true;
            Item item = auctionsDBContext.Items.Where(item => item.ItemID == offer.ItemID).FirstOrDefault();
            item.AcceptedOfferID = offer.OfferID;
             auctionsDBContext.SaveChanges();

            return Ok(offerID);
        }
        [HttpGet("reject-offer/{offerID}")]
        public ActionResult RejectOffer(int offerID)
        {
            Offer offer = auctionsDBContext.Offers
                .Where(offer => offer.OfferID == offerID)
                .FirstOrDefault();
            auctionsDBContext.Offers.Remove(offer);

            

            auctionsDBContext.SaveChanges();

            return Ok();

        }
    }
}
