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

namespace AuctionsAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private AuctionsDBContext auctionsDBContext;
        private ITokenAuthorization userAuthorization;
        public UserController(
            AuctionsDBContext _auctionsDBContext,
            ITokenAuthorization _userAuthorization)
        {
            auctionsDBContext = _auctionsDBContext;
            userAuthorization = _userAuthorization;
        }

        [HttpPost("complete-account")]
        [Authorize]
        public ActionResult CompleteAccount([FromBody] NewUser newUser)
        {
            User user = new User()
            {
                UserID = userAuthorization.GetCurrentUser(User.Claims),
                Name = newUser.Name,
                Lastname = newUser.Lastname,
                EmailForContact = newUser.EmailForContact,
                PhoneNumber = newUser.PhoneNumber,
                JoinDate = DateTime.Now,
                LastTimeOnline = DateTime.Now
            };

            auctionsDBContext.Users.Add(user);
            auctionsDBContext.SaveChanges();
            return Ok(user.UserID);
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

       


    }
}
