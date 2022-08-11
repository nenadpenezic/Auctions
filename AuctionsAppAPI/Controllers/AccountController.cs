﻿using AuctionsAppAPI.DTO;
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
        public ActionResult AddAccount([FromBody] NewAccount newAccount)
        {
            string verificationString = GenerateVerificationString();

            Account existingAccoutn = auctionsDBContext.Accounts
                .Where(acc => acc.Email == newAccount.Email)
                .FirstOrDefault();

            if (existingAccoutn != null)
                return BadRequest("Account with this email address aready exist!");


            Account account = new Account()
            {
                Email = newAccount.Email,
                Password = newAccount.Password,
                IsAccountVerifyed = false,
                VerificationString = verificationString
            };

            auctionsDBContext.Accounts.Add(account);
            auctionsDBContext.SaveChanges();

            string confirmationString = "<a href='" + "https://localhost:44301/api/Account/verify/"+ account.AccountID + "/" + verificationString +"'/>Click here to verify your accout</a>";
            emailClient.SendEmail(account.Email, confirmationString);

            return Ok("Check your email to confirm email adress and finish registration");
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
        public ActionResult LogIn([FromBody] LogIn login)
        {
            Account account = auctionsDBContext.Accounts
                .Where(acc => acc.Email == login.Email && acc.Password == login.Password)
                .FirstOrDefault();

            if (account == null)
                return BadRequest("Wrong Email or Password!");

            if (!account.IsAccountVerifyed)
                return BadRequest("Account must be verified first");

            //User user = auctionsDBContext.Users.Find(account.AccountID);

            AuthenticatedUser authenticatedUser = auctionsDBContext.Users.Where(user => user.UserID == account.AccountID)
                .Select(user => new AuthenticatedUser
                {
                    UserID = user.UserID,
                    Name = user.Name,
                    Lastname = user.Lastname,
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


            //if (user == null)
              //  authenticatedUser.IsAccountComplete = false;
           // else
           // {
              //  List<NotificationDTO> notifications = auctionsDBContext
                 //   .Notifications
                   // .Where(notification => notification.UserID == user.UserID)
                   // .Select(notification => new NotificationDTO { 
                    //    NotificationID = notification.NotificationID,
                      //  NotificationText = notification.NotificationText,
                      //  ArriveDate = notification.ArriveDate,
                      //  Open = notification.Open
                   // })
                   // .ToList();

               // authenticatedUser.UserID = account.AccountID;
               // authenticatedUser.IsAccountComplete = true;
              //  authenticatedUser.Name = user.Name;
              //  authenticatedUser.Lastname = user.Lastname;
               // authenticatedUser.Notifications = notifications;
           // }

            string tokenString = userAuthorization.GenerateToken(account.AccountID.ToString());
            return Ok(new { Token = tokenString, userObj = authenticatedUser});
        }

        [HttpGet("block-account/{accountID}")]
        public ActionResult BlockAccount(int accountID)
        {
            Account account = auctionsDBContext.Accounts
                .Where(account => account.AccountID == accountID)
                .FirstOrDefault();
            account.IsBlocked = true;
            if(auctionsDBContext.SaveChanges() > 0)
                return Ok();

            return BadRequest();
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
