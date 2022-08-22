using AuctionsAppAPI.Authorization;
using AuctionsAppAPI.DTO;
using AuctionsAppAPI.EmailClient;
using DataLayer.DatabaseConfiguration;
using DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuctionsAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private AuctionsDBContext auctionsDBContext;
        private ITokenAuthorization tokenAuthorization;
        private IWebHostEnvironment webHostEnvironment;
        private IEmailClient emailClient;
        public ItemController(
            AuctionsDBContext _auctionsDBContext,
            ITokenAuthorization _tokenAuthorization,
            IWebHostEnvironment _webHostEnvironment,
            IEmailClient _emailClient
            )
        {
            auctionsDBContext = _auctionsDBContext;
            tokenAuthorization = _tokenAuthorization;
            webHostEnvironment = _webHostEnvironment;
            emailClient = _emailClient;
        }

        [HttpPost("add-item")]
        [Authorize]
        public ActionResult AddItem([FromForm ] NewItem newItem)
        {
            if(!ModelState.IsValid)
                return BadRequest("");

            Item item = new Item(){
                ItemName = newItem.ItemName,
                OwnerID = tokenAuthorization.GetCurrentUser(User.Claims),
                Description = newItem.Description,
                AddedDate = DateTime.Now,
                CategoryID = newItem.CategoryID,
                CurrentPrice = newItem.Price,
                StartPrice = newItem.Price,
                CurrencyID = newItem.CurrencyID
            };

            List<NewItemSpecification> newItemSpecifications = JsonConvert
                .DeserializeObject<List<NewItemSpecification>>(newItem.NewItemSpecifications);
            ICollection<ItemSpecification> itemSpecifications = new List<ItemSpecification>();

            foreach (NewItemSpecification element in newItemSpecifications)
                itemSpecifications.Add(new ItemSpecification(){
                    SpecificationName = element.SpecificationName,
                    SpecificationValue = element.SpecificationValue,
                });
            
            item.ItemSpecifications = itemSpecifications;

            ICollection<ItemPhoto> itemPhotos  = new List<ItemPhoto>();

            foreach (IFormFile element in newItem.ItemPictures){
                itemPhotos.Add(new ItemPhoto
                {
                   PhotoUrl = UploadItemPhotos(element)
                });
            }
            item.ItemPhotos = itemPhotos;

            auctionsDBContext.Items.Add(item);
            auctionsDBContext.SaveChanges();

            return Ok();
        }


        [HttpGet("get-user-items/{userID}")]
        public ActionResult GetUserItems(int userID)
        {
            List<ItemPreview> userItems = auctionsDBContext.Items
                .Where(item => item.OwnerID == userID).Select(item=>new ItemPreview {
                    ItemID = item.ItemID,
                    ItemName = item.ItemName,
                    CurrentPrice = item.CurrentPrice,
                    IsSold = item.AcceptedOffer != null ? true : false,
                    UserID = item.Owner.UserID,
                    Name = item.Owner.Name,
                    Lastname = item.Owner.Lastname,
                    Category = item.Category.CategoryName,
                    PreviewImage = item.ItemPhotos.Select(photo => photo.PhotoUrl).FirstOrDefault(),

                }).ToList();

            return Ok(userItems);
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
                    .Select(offer=> new OfferDetails{

                        OfferID = offer.OfferID,
                        ItemID = offer.ItemID,
                        UserID = offer.UserID,
                        Name = offer.User.Name,
                        Lastname = offer.User.Lastname,
                        AddedDate = offer.OfferDate,
                        Value = offer.Value,
                        IsAccepted = offer.isAccepted,
                        Currency = offer.Item.Currency.CurrencyName

                    }).ToList(),
                    AddedDate = item.AddedDate,
                    CurrentPrice = item.CurrentPrice,
                    ItemPhotos = item.ItemPhotos.Select(photo=>photo.PhotoUrl).ToList()
                }).FirstOrDefault();

            return Ok(itemDetails);
        }

        [HttpPost("delete-item/{itemID}")]
        [Authorize]
        public ActionResult DeleteItem(int itemID,[FromForm] string notificationText)
        {
            Claim role = User.Claims
                .FirstOrDefault(claim => claim.Type.ToString()
                .Equals("Role", StringComparison.InvariantCultureIgnoreCase));

            if (!String.Equals(role.Value, "Administrator"))
                return Unauthorized();

            Item item = auctionsDBContext.Items
                .Include(item=>item.Owner)
                .Include(item=>item.AcceptedOffer)
                .Where(item => item.ItemID == itemID).FirstOrDefault();

            string contactEmail = item.Owner.EmailForContact;

            if(item.AcceptedOffer != null)
            {
                item.AcceptedOffer = null;
                auctionsDBContext.SaveChanges();
            }

            auctionsDBContext.Offers
                .RemoveRange(auctionsDBContext.Offers
                .Where(offer => offer.ItemID == itemID));

            auctionsDBContext.ItemPhotos
                .RemoveRange(auctionsDBContext.ItemPhotos
                .Where(itemPhoto => itemPhoto.ItemID == itemID));

            auctionsDBContext.ItemSpecifications
                .RemoveRange(auctionsDBContext.ItemSpecifications
                .Where(specs => specs.ItemID == itemID));
            
            auctionsDBContext.Items.Remove(item);
            if(auctionsDBContext.SaveChanges()<=0)
                return BadRequest();
            emailClient.SendEmail(contactEmail, notificationText);

            return Ok();
        }



        [HttpGet("administrator/get-items")]
        public ActionResult AdministratorSearch([FromQuery] string name)
        {

            List<AdministratorItemDetails> administratorItemDetails = auctionsDBContext.Items
             .Where(user => user.ItemName.Contains(name)).
             Select(item => new AdministratorItemDetails
             {
                    ItemID = item.ItemID,
                    Name = item.Owner.Name,
                    Lastname = item.Owner.Lastname,
                    ItemName = item.ItemName,
                    CurrentPrice = item.CurrentPrice,
                    IsItemBlocked = item.IsItemBlocked,
                    AddedDate = item.AddedDate,
                    IsSold = item.AcceptedOffer==null ? false : true,
                    Photo = item.ItemPhotos.Select(photo=>photo.PhotoUrl).FirstOrDefault(),
                    UserPhoto = item.Owner.ProfilePhoto

             })
             .ToList();

            return Ok(administratorItemDetails);
        }

        [HttpPut("administrator/block-status/{itemID}/action/{type}")]
        [Authorize]
        public ActionResult SwitchBlockStatus(int itemID,string type, [FromForm] string notificationText)
        {
            Claim role = User.Claims
             .FirstOrDefault(claim => claim.Type.ToString()
             .Equals("Role", StringComparison.InvariantCultureIgnoreCase));

            if (!String.Equals(role.Value, "Administrator"))
                return Unauthorized();

            Item item = auctionsDBContext.Items
                .Include(item=>item.Owner)
                .Where(item => item.ItemID == itemID)
                .FirstOrDefault();

            if (item.IsItemBlocked && String.Equals(type.ToLower(), "block"))
                return BadRequest("");

            if (!item.IsItemBlocked && String.Equals(type.ToLower(), "unblock"))
                return BadRequest("");

            item.IsItemBlocked = !item.IsItemBlocked;
            if (auctionsDBContext.SaveChanges() <= 0)
                return BadRequest("");

            emailClient.SendEmail(item.Owner.EmailForContact, notificationText);

            return Ok();
        }

        [HttpGet("get-items")]
        public ActionResult GetItemsForDisplay([FromQuery] ItemSearchQuery query)
        {
            IQueryable<Item> userList = auctionsDBContext.Items;

            if (query.ItemName != null)
                userList = userList.Where(item => item.ItemName.Contains(query.ItemName));

            if(query.StartPrice>=0)
                userList = userList.Where(item => item.CurrentPrice >= query.StartPrice);

            if (query.EndPrice > 0 && query.EndPrice > query.StartPrice)
                userList = userList.Where(item => item.CurrentPrice <= query.EndPrice);

            if(!query.IsSold)
                userList = userList.Where(item => item.AcceptedOffer == null);

            if(query.CategoryID > 0)
            userList = userList.Where(item => item.CategoryID == query.CategoryID);

            List<ItemPreview> itemPreviews = userList.Select(item => new ItemPreview
            {
                ItemID = item.ItemID,
                ItemName = item.ItemName,
                CurrentPrice = item.CurrentPrice,
                IsSold = item.AcceptedOffer != null ? true : false,
                UserID = item.Owner.UserID,
                Name = item.Owner.Name,
                Lastname = item.Owner.Lastname,
                Category = item.Category.CategoryName,
                PreviewImage = item.ItemPhotos.Select(photo => photo.PhotoUrl).FirstOrDefault(),
                }
                ).ToList();

            return Ok(itemPreviews);
        }
        private string UploadItemPhotos(IFormFile formFile)
        {

            string directoryPath = Path.Combine(webHostEnvironment.ContentRootPath, "Images");

            using (var stream = new FileStream(Path.Combine(directoryPath, formFile.FileName), FileMode.Create))
            {
                formFile.CopyTo(stream);
            }

            return formFile.FileName;
        }
    }
}
