using AuctionsAppAPI.DTO;
using AuctionsAppAPI.Authorization;
using DataLayer.DatabaseConfiguration;
using DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace AuctionsAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private AuctionsDBContext auctionsDBContext;
        private ITokenAuthorization userAuthorization;
        private IWebHostEnvironment webHostEnvironment;
        public UserController(
            AuctionsDBContext _auctionsDBContext,
            ITokenAuthorization _userAuthorization,
             IWebHostEnvironment _webHostEnvironment
            )
        {
            auctionsDBContext = _auctionsDBContext;
            userAuthorization = _userAuthorization;
            webHostEnvironment = _webHostEnvironment;
        }

        [HttpPost("complete-account")]
        [Authorize]
        public ActionResult CompleteAccount([FromForm] NewUser newUser)
        {
         

            User user = new User()
            {
                UserID = userAuthorization.GetCurrentUser(User.Claims),
                Name = newUser.Name,
                Lastname = newUser.Lastname,
                EmailForContact = newUser.EmailForContact,
                PhoneNumber = newUser.PhoneNumber,
                JoinDate = DateTime.Now,
                LastTimeOnline = DateTime.Now,
                ProfilePhoto = UploadProfilePhoto(newUser.ProfilePicture)
            };

            auctionsDBContext.Users.Add(user);
            auctionsDBContext.SaveChanges();

            AuthenticatedUser authenticatedUser = auctionsDBContext.Users.Where(authUser => authUser.UserID == user.UserID)
                .Select(user => new AuthenticatedUser
                {
                    UserID = user.UserID,
                    Name = user.Name,
                    Lastname = user.Lastname,
                    ProfilePhoto = user.ProfilePhoto,
                    Notifications = user.Notifications.Select(notification => new NotificationDTO
                    {
                        NotificationID = notification.NotificationID,
                        NotificationText = notification.NotificationText,
                        ArriveDate = notification.ArriveDate,
                        Open = notification.Open,
                        

                    })
                    .ToList(),
                    IsAccountComplete = true
                }).FirstOrDefault();

            return Ok(authenticatedUser);
        }

        [HttpGet("user-profile/{userID}")]
        public ActionResult GetUserProfile(int userID)
        {
            UserProfile userProfile = auctionsDBContext.Users.Where(user => user.UserID == userID).
                Select(userProfile => new UserProfile {
                    Name = userProfile.Name,
                    Lastname = userProfile.Lastname,
                    EmailForContact = userProfile.EmailForContact,
                    PhoneNumber = userProfile.PhoneNumber,
                    JoinDate = userProfile.JoinDate,
                    LastTimeOnline = userProfile.LastTimeOnline,
                    //AverageGrade = userProfile.UserPersonalReviews).Select(ups => ups.Grade).Average(),
                    NumberOfReviews = userProfile.UserPersonalReviews.Count,
                    NumberOfItemsOnSale = userProfile.Items.Count,

                }).FirstOrDefault();
            return Ok(userProfile);
        }



        [HttpPost("administrator-users")]
        public ActionResult GetUsersForAdministrator([FromBody] string name)
        {
            List<string> nameSplit = name.Split(' ').ToList();

            if (nameSplit.Count == 1)
            {
                nameSplit[0] = name;
                nameSplit.Add("");
            }
                

            List<UserAdministrationProfile> userAdministrationProfiles = auctionsDBContext.Users
                .Where(user => user.Name.Contains(nameSplit[0]) && user.Lastname.Contains(nameSplit[1])).
                Select(user=>new UserAdministrationProfile
                {   UserID =user.UserID,
                    Name = user.Name,
                    Lastname = user.Lastname,
                    EmailForContact = user.EmailForContact,
                    PhoneNumber = user.PhoneNumber,
                    IsBlocked = user.Account.IsBlocked
                })
                .ToList();
            return Ok(userAdministrationProfiles);
        }

        private string UploadProfilePhoto(IFormFile formFile)
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
