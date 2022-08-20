using AuctionsAppAPI.Authorization;
using AuctionsAppAPI.DTO;
using AuctionsAppAPI.EmailClient;
using DataLayer.DatabaseConfiguration;
using DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private IEmailClient emailClient;
        public OfferController(
            AuctionsDBContext _auctionsDBContext,
            ITokenAuthorization _tokenAuthorization,
             IEmailClient _emailClient)
        {
            auctionsDBContext = _auctionsDBContext;
            tokenAuthorization = _tokenAuthorization;
            emailClient = _emailClient;
        }

        [HttpPost("add-offer")]
        [Authorize]
        public ActionResult AddOffer([FromForm] NewOffer newOffer)
        {
            int UserID = tokenAuthorization.GetCurrentUser(User.Claims);

            Offer lastOffer = auctionsDBContext.Offers
                .Include(offer=>offer.Item)
                .OrderByDescending(offer => offer.Value)
                .Where(offer=>offer.ItemID == newOffer.ItemID)
                .FirstOrDefault();

            if(lastOffer != null)
            {
                if (lastOffer.isAccepted)
                    return BadRequest("");

                if (lastOffer.Value > newOffer.Value)
                    return BadRequest("");

                if(lastOffer.UserID == UserID || lastOffer.Item.OwnerID == UserID)
                    return BadRequest("");
            }

            Offer offer = new Offer()
            {
               ItemID = newOffer.ItemID,
               UserID = UserID,
               Value = newOffer.Value,
               OfferDate = DateTime.Now
            };

            auctionsDBContext.Offers.Add(offer);

            Item offerItem = auctionsDBContext.Items.Find(newOffer.ItemID);
            offerItem.CurrentPrice = newOffer.Value;

            auctionsDBContext.SaveChanges();

            OfferDetails itemDetailsOffer = auctionsDBContext.Offers
                .Where(off => off.OfferID == offer.OfferID)
                .Select(offer => new OfferDetails()
            {
                OfferID = offer.OfferID,
                Name = offer.User.Name,
                Lastname = offer.User.Lastname,
                Value = offer.Value,
                 AddedDate = offer.OfferDate,
                IsAccepted = offer.isAccepted,
                Currency = offer.Item.Currency.CurrencyName
                }).FirstOrDefault();

            if(lastOffer==null)
                return Ok(itemDetailsOffer);

            Notification notification = new Notification()
            {
                UserID = lastOffer.UserID,
                NotificationTitle = "Licitacija",
                NotificationText = "Vaša ponuda koju ste licitirali za predmet " 
                + offerItem.ItemName + " nije vise vodeća!",
                ArriveDate = DateTime.Now,
                Open = false,
            };

            auctionsDBContext.Notifications.Add(notification);
            auctionsDBContext.SaveChanges();

            return Ok(itemDetailsOffer);
        }
        [HttpGet("item-offers/{itemID}")]
        public ActionResult GetProductOffers(int itemID)
        {
            List<OfferDetails> offers = auctionsDBContext.Offers.OrderByDescending(order => order.Value)
                    .Select(offer => new OfferDetails
                    {
                        OfferID = offer.OfferID,
                        ItemID = offer.ItemID,
                        UserID = offer.UserID,
                        Name = offer.User.Name,
                        Lastname = offer.User.Lastname,
                        AddedDate = offer.OfferDate,
                        Value = offer.Value,
                        IsAccepted = offer.isAccepted,
                        Currency = offer.Item.Currency.CurrencyName

                    }).ToList();
            return Ok(offers);
        }

        [HttpGet("accept-offer/{offerID}")]
        [Authorize]
        public ActionResult AcceptOffer(int offerID)
        {
            int currentUser = tokenAuthorization.GetCurrentUser(User.Claims);

            Offer offer = auctionsDBContext.Offers
                .Include(offer => offer.User)
                .Where(offer => offer.OfferID == offerID)
                .FirstOrDefault();

            Offer acceptedOffer = auctionsDBContext.Offers
                .Where(acceptedOffer => (acceptedOffer.ItemID == offer.ItemID) && offer.isAccepted)
                .FirstOrDefault();

            if (acceptedOffer != null)
                return BadRequest("");

            Item item = auctionsDBContext.Items
                .Include(item => item.Owner)
                .Where(item => item.ItemID == offer.ItemID)
                .FirstOrDefault();

            offer.isAccepted = true;

            item.AcceptedOfferID = offer.OfferID;
            auctionsDBContext.SaveChanges();

            string message = "Vaša ponuda za " + item.ItemName + " je prihvaćena." +
                " Za dalji dogovor sa prodavcem mozete ga kontaktirati putem email adrese: "
                + item.Owner.EmailForContact + " ili telefonom: " + item.Owner.PhoneNumber;

            emailClient.SendEmail(offer.User.EmailForContact, message);

            Notification notification = new Notification()
            {
                NotificationTitle = "Prihvaćena ponuda za " + item.ItemName,
                NotificationText = "Na vasu email adresu poslat je email sa detaljima.",
                ArriveDate = DateTime.Now,
                Open = false,
                UserID = offer.User.UserID
                
            };

            return Ok(offerID);
        }

        [HttpDelete("reject-offer/{offerID}")]
        public ActionResult RejectOffer(int offerID)
        {
            Offer offer = auctionsDBContext.Offers
                .Where(offer => offer.OfferID == offerID)
                .FirstOrDefault();

            Item item = auctionsDBContext.Items.Find(offer.ItemID);

            Offer secondHighestOffer = auctionsDBContext.Offers
                .OrderByDescending(offer => offer.Value)
                .Where(offer => offer.ItemID == item.ItemID)
                .Skip(1)
                .FirstOrDefault();

           if(secondHighestOffer != null)
                item.CurrentPrice = secondHighestOffer.Value;
           else
                item.CurrentPrice = item.StartPrice;

            item.AcceptedOffer = null;
           auctionsDBContext.Offers.Remove(offer);

            auctionsDBContext.SaveChanges();

            return Ok();
        }
    }
}
