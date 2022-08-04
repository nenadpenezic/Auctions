using AuctionsAppAPI.Authorization;
using AuctionsAppAPI.DTO;
using DataLayer.DatabaseConfiguration;
using DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
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
    public class UserReviewController : ControllerBase
    {
        private AuctionsDBContext auctionsDBContext;
        private ITokenAuthorization tokenAuthorization;
        public UserReviewController(
            AuctionsDBContext _auctionsDBContext,
            ITokenAuthorization _tokenAuthorization)
        {
            auctionsDBContext = _auctionsDBContext;
            tokenAuthorization = _tokenAuthorization;
        }

        [HttpPost("add-review/{userID}")]
        [Authorize]
        public ActionResult AddReview([FromBody] NewReview newReview, int userID)
        {

            UserReview userReview = new UserReview()
            {
                ReviewerID = tokenAuthorization.GetCurrentUser(User.Claims),
                UserID = userID,
                Comment = newReview.Comment,
                Grade = newReview.Grade,
                ReviewDate = DateTime.Now
            };

            auctionsDBContext.UserReviews.Add(userReview);

            if (auctionsDBContext.SaveChanges() > 0)
                return Ok("Review successfuly added");

            return StatusCode(500);
        }

        [HttpPost("get-reviews/{userID}")]
        [Authorize]
        public ActionResult GetUserReviews(int userID)
        {
            List<Review> userReviews = auctionsDBContext.UserReviews
                .Where(review => review.UserID == userID)
                .Select(review=>new Review
                {
                    Name = review.User.Name,
                    Lastname = review.User.Lastname,
                    Grade = review.Grade,
                    Comment = review.Comment,
                    ReviewDate = review.ReviewDate
                })
                .ToList();

            return Ok(userReviews);
        }

    }
}
