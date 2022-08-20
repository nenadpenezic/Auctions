using AuctionsAppAPI.DTO;
using AuctionsAppAPI.EmailClient;
using AuctionsAppAPI.Authorization;
using DataLayer.DatabaseConfiguration;
using DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace AuctionsAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private IEmailClient emailClient;
        private AuctionsDBContext auctionsDBContext;
        private ITokenAuthorization userAuthorization;
        public AccountController(
            IEmailClient _emailClient, 
            AuctionsDBContext _auctionsDBContext, 
            ITokenAuthorization _userAuthorization)
        {
            emailClient = _emailClient;
            auctionsDBContext = _auctionsDBContext;
            userAuthorization = _userAuthorization;
        }


        [HttpPost("add-account")]
        public ActionResult AddAccount([FromForm] NewAccount newAccount)
        {
            if (newAccount.Password != newAccount.ConfirmedPassword)
                return BadRequest();

            Account existingAccoutn = auctionsDBContext.Accounts
                .Where(acc => acc.Email == newAccount.Email)
                .FirstOrDefault();

            if (existingAccoutn != null)
                return BadRequest();

            string verificationString = GenerateVerificationString();

            Account account = new Account()
            {
                Email = newAccount.Email,
                Password = newAccount.Password,
                IsAccountVerifyed = false,
                VerificationString = verificationString,
                IsBlocked = false,
                RoleID = 1
            };

            auctionsDBContext.Accounts.Add(account);
            auctionsDBContext.SaveChanges();

            string confirmationString = "<a href='" + "https://localhost:44301/api/Account/verify/"+ 
                account.AccountID + "/" + verificationString +"'/>Click here to verify your accout</a>";
            emailClient.SendEmail(account.Email, confirmationString);

            return Ok();
        }

        [HttpGet("verify/{userID}/{verificationString}")]
        public ActionResult VerifyAccount(int userID,string verificationString)
        {
            var account = auctionsDBContext.Accounts
                .Where(acc => acc.AccountID == userID)
                .FirstOrDefault();
            account.IsAccountVerifyed = true;
            auctionsDBContext.SaveChanges();

            return Ok(verificationString);
        }

        [HttpPost("log-in")]
        public ActionResult LogIn([FromForm] LogIn login)
        {
            Account account = auctionsDBContext.Accounts
                .Include(account=>account.Role)
                .Where(acc => acc.Email == login.Email && acc.Password == login.Password)
                .FirstOrDefault();

            if (account == null)
                return BadRequest("Wrong Email or Password!");

            if (!account.IsAccountVerifyed)
                return BadRequest("Account must be verified first");

            AuthenticatedUser authenticatedUser = auctionsDBContext.Users.Where(user => user.UserID == account.AccountID)
                .Select(user => new AuthenticatedUser
                {
                    UserID = user.UserID,
                    Name = user.Name,
                    Lastname = user.Lastname,
                    ProfilePhoto = user.ProfilePhoto,
                    Notifications = user.Notifications.Select(notification => new NotificationDetails
                    {
                        NotificationID = notification.NotificationID,
                        NotificationText = notification.NotificationText,
                        ArriveDate = notification.ArriveDate,
                        Open = notification.Open,
                    })
                    .ToList(),
                    IsAccountComplete = true,
                    Role = user.Account.Role.RoldeName
                    }).FirstOrDefault();

            string tokenString = userAuthorization.GenerateToken(account.AccountID.ToString(), account.Role.RoldeName);
            return Ok(new { Token = tokenString, userObj = authenticatedUser});
        }

        [HttpPut("administrator/{accountID}/block/{type}")]
        [Authorize]
        public ActionResult SwitchBlockStatus(int accountID, string type, [FromForm] string notificationText)
        {
            Claim role = User.Claims
                .FirstOrDefault(claim => claim.Type.ToString()
                .Equals("Role", StringComparison.InvariantCultureIgnoreCase));

            if (!String.Equals(role.Value, "Administrator"))
                return Unauthorized();

            Account account = auctionsDBContext.Accounts
                .Where(account => account.AccountID == accountID)
                .FirstOrDefault();

            if (account.IsBlocked && String.Equals(type.ToLower(), "block"))
                return BadRequest("");

            if (!account.IsBlocked && String.Equals(type.ToLower(), "unblock"))
                return BadRequest("");


            account.IsBlocked = !account.IsBlocked;
            auctionsDBContext.SaveChanges();

           
            emailClient.SendEmail(account.Email, notificationText);

            return Ok();
        }

        [HttpGet("unblock-account/{accountID}")]
        public ActionResult UnblockAccount(int accountID)
        {
            Account account = auctionsDBContext.Accounts
                .Where(account => account.AccountID == accountID)
                .FirstOrDefault();
            account.IsBlocked = false;
            if (auctionsDBContext.SaveChanges() > 0)
                return Ok();

            return BadRequest();
        }


        private string GenerateVerificationString()
        {
            char[] allowedCharacters = "qwertyuiopasdfghjklzxcvbnm1234567890".ToCharArray();
            Random random = new Random();
            string verificationString = "";
            for (int i = 0; i < 195; i++)
                verificationString += allowedCharacters[random.Next(0, allowedCharacters.Length)];
            return verificationString;
        }

    }

  
}
