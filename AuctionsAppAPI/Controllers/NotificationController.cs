using AuctionsAppAPI.Authorization;
using AuctionsAppAPI.EmailClient;
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
    public class NotificationController : ControllerBase
    {
        private IEmailClient emailClient;
        private AuctionsDBContext auctionsDBContext;
        private ITokenAuthorization userAuthorization;
        public NotificationController(
            IEmailClient _emailClient,
            AuctionsDBContext _auctionsDBContext,
            ITokenAuthorization _userAuthorization)
        {
            emailClient = _emailClient;
            auctionsDBContext = _auctionsDBContext;
            userAuthorization = _userAuthorization;
        }

        [HttpPost("administrator/send-notification/{userID}")]
        public ActionResult AddNotification([FromBody] string notificationText, int userID)
        {
            Notification notification = new Notification()
            {
                UserID = userID,
                NotificationText = notificationText,
                ArriveDate = DateTime.Now,
                Open = false
            };
            auctionsDBContext.Notifications.Add(notification);
            if (auctionsDBContext.SaveChanges() > 0)
                return Ok(notificationText);

            return BadRequest();
        }
    }
}
