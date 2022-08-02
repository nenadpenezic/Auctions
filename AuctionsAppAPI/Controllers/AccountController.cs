using AuctionsAppAPI.DTO;
using AuctionsAppAPI.EmailClient;
using AuctionsAppAPI.Services;
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
    public class AccountController : ControllerBase
    {

        private IEmailClient emailClient;
        private AuctionsDBContext auctionsDBContext;
        public AccountController(IEmailClient _emailClient, 
            AuctionsDBContext _auctionsDBContext)
        {
            emailClient = _emailClient;
            auctionsDBContext = _auctionsDBContext;
        }
        [HttpPost("add-account")]
        public ActionResult AddAccount([FromBody] NewAccount newAccount)
        {
            string verificationString = StringGenerator.GenerateVerificationString();

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

            return Ok(account);
        }

        [HttpGet("verify/{userID}/{verificationString}")]
        public ActionResult VerifyAccount(int userID,string verificationString)
        {
            var account = auctionsDBContext.Accounts.Where(acc => acc.AccountID == userID).FirstOrDefault();
            account.IsAccountVerifyed = true;
            auctionsDBContext.SaveChanges();
            return Ok(verificationString);
        }

    }

  
}
