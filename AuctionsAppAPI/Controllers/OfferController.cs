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

        [HttpPost("add-offer")]
        [Authorize]
        public ActionResult AddOffer([FromBody] NewOffer newOffer)
        {
            int UserID = tokenAuthorization.GetCurrentUser(User.Claims);

            ItemAuctionParticipant existingParticipant = auctionsDBContext.AuctionParticipants
                .Where(p => p.UserID == UserID && p.ItemID == newOffer.ItemID)
                .FirstOrDefault();

            if (existingParticipant == null)
                auctionsDBContext.AuctionParticipants.Add(new ItemAuctionParticipant()
                {
                    UserID = UserID,
                    ItemID = newOffer.ItemID
                });
            
            Offer offer = new Offer()
            {
               ItemID = newOffer.ItemID,
               UserID = UserID,
               Value = newOffer.Value,
               OfferDate = DateTime.Now
            };

            auctionsDBContext.Offers.Add(offer);
            if(auctionsDBContext.SaveChanges()>0)
                return Ok();

            return StatusCode(500);

        }
        [HttpGet("item-offers/{offerID}")]
        public ActionResult GetProductOffers(int offerID)
        {
            List<OfferView> offers = auctionsDBContext.Offers.Where(offer => offer.OfferID == offerID)
                .Select(offer => new OfferView
                {
                    Name = offer.ItemAuctionParticipant.User.Name,
                    Lastname = offer.ItemAuctionParticipant.User.Lastname,
                    Value = offer.Value,
                    OfferDate = offer.OfferDate
                }).ToList();
            return Ok(offers);
        }
    }
}
