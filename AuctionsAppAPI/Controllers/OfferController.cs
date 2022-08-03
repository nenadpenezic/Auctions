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
        public ActionResult AddOffer([FromBody] NewOffer newOffer)
        {
            int UserID = tokenAuthorization.GetCurrentUser(User.Claims);

            ItemAuctionParticipant existingParticipant = auctionsDBContext.AuctionParticipants
                .Where(p => p.UserID == UserID && p.ItemID == newOffer.ItemID)
                .FirstOrDefault();

            if (existingParticipant == null)
            {
                ItemAuctionParticipant participant = new ItemAuctionParticipant()
                {
                    UserID = UserID,
                    ItemID = newOffer.ItemID
                };

                auctionsDBContext.AuctionParticipants.Add(participant);
            }
            
            Offer offer = new Offer()
            {
               ItemID = newOffer.ItemID,
               UserID = UserID,
               Value = newOffer.Value,
               OfferDate = DateTime.Now
            };

            auctionsDBContext.Offers.Add(offer);
            auctionsDBContext.SaveChanges();

            return Ok();
        }
    }
}
