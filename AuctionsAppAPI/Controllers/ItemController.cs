using AuctionsAppAPI.Authorization;
using AuctionsAppAPI.DTO;
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
        public ItemController(
            AuctionsDBContext _auctionsDBContext,
            ITokenAuthorization _tokenAuthorization,
            IWebHostEnvironment _webHostEnvironment
            )
        {
            auctionsDBContext = _auctionsDBContext;
            tokenAuthorization = _tokenAuthorization;
            webHostEnvironment = _webHostEnvironment;
        }

        [HttpPost("add-item")]
        [Authorize]
        public ActionResult AddItem([FromForm ] NewItem newItem)
        {
            Item item = new Item(){
                ItemName = newItem.ItemName,
                OwnerID = tokenAuthorization.GetCurrentUser(User.Claims),
                Description = newItem.Description,
                AddedDate = DateTime.Now,
                CategoryID = newItem.CategoryID,
                Price = newItem.Price,
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
                    CurrentPrice = item.Price,
                    IsSold = item.AcceptedOffer != null ? true : false,
                    UserID = item.Owner.UserID,
                    Name = item.Owner.Name,
                    Lastname = item.Owner.Lastname,
                    Category = item.Category.CategoryName,
                    PreviewImage = item.ItemPhotos.Select(photo => photo.PhotoUrl).FirstOrDefault(),
                    Price = item.Price

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
                    Price = item.Price,
                    ItemPhotos = item.ItemPhotos.Select(photo=>photo.PhotoUrl).ToList()
                }).FirstOrDefault();

            return Ok(itemDetails);
        }

        [HttpPost("delete-item/{itemID}")]
        public ActionResult DeleteItem(int itemID,[FromBody] string notificationText)
        {
            Item item = auctionsDBContext.Items
                .Include(item=>item.AcceptedOffer)
                .Where(item => item.ItemID == itemID).FirstOrDefault();

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
            if(auctionsDBContext.SaveChanges()>0)
                return Ok();

            return BadRequest();
        }

        [HttpGet("administrator/get-items/{searchQuery}")]
        public ActionResult GetItemsForAdministrator(string searchQuery)
        {

            List<AdministratorItemDetails> administratorItemDetails = auctionsDBContext.Items
             .Where(user => user.ItemName.Contains(searchQuery)).
             Select(item => new AdministratorItemDetails
             {
                    ItemID = item.ItemID,
                    Name = item.Owner.Name,
                    Lastname = item.Owner.Lastname,
                    ItemName = item.ItemName,
                    Price = item.Price,
                    IsItemBlocked = item.IsItemBlocked,
                    AddedDate = item.AddedDate,
                    IsSold = item.AcceptedOffer==null ? false : true
             })
             .ToList();

            return Ok(administratorItemDetails);
        }

        [HttpPost("block-item/{itemID}")]
        public ActionResult BlockItem(int itemID,[FromBody] string notificationText)
        {
            Item item = auctionsDBContext.Items.Where(item => item.ItemID == itemID).FirstOrDefault();
            item.IsItemBlocked = true;
            if (auctionsDBContext.SaveChanges() > 0)
                return Ok(item);

            return BadRequest();
        }
        [HttpPost("unblock-item/{itemID}")]
        public ActionResult UnblockItem(int itemID, [FromBody] string notificationText)
        {
            Item item = auctionsDBContext.Items.Where(item => item.ItemID == itemID).FirstOrDefault();
            item.IsItemBlocked = false;
            if (auctionsDBContext.SaveChanges() > 0)
                return Ok();

            return BadRequest();
        }
        [HttpGet("get-items")]
        public ActionResult GetItemsForDisplay([FromQuery] ItemSearchQuery query)
        {

            IQueryable<Item> userList = auctionsDBContext.Items;

            if (query.ItemName != null)
                userList = userList.Where(item => item.ItemName.Contains(query.ItemName));

            if(query.StartPrice>=0)
                userList = userList.Where(item => item.Price>=query.StartPrice);

            if (query.EndPrice > 0)
                userList = userList.Where(item => item.Price <= query.EndPrice);

            if(!query.IsSold)
                userList = userList.Where(item => (item.AcceptedOffer != null) == query.IsSold);

            //if(query.CategoryID > 0)
            //userList = userList.Where(item => item.CategoryID == query.CategoryID);


            List<ItemPreview> itemPreviews = userList.Select(item => new ItemPreview
            {
                ItemID = item.ItemID,
                ItemName = item.ItemName,
                CurrentPrice = item.Price,
                IsSold = item.AcceptedOffer != null ? true : false,
                UserID = item.Owner.UserID,
                Name = item.Owner.Name,
                Lastname = item.Owner.Lastname,
                Category = item.Category.CategoryName,
                PreviewImage = item.ItemPhotos.Select(photo => photo.PhotoUrl).FirstOrDefault(),
                Price = item.Price
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
