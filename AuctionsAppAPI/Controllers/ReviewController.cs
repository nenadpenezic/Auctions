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
    public class ReviewController : ControllerBase
    {
        private AuctionsDBContext auctionsDBContext;
        private ITokenAuthorization tokenAuthorization;
        public ReviewController(
            AuctionsDBContext _auctionsDBContext,
            ITokenAuthorization _tokenAuthorization)
        {
            auctionsDBContext = _auctionsDBContext;
            tokenAuthorization = _tokenAuthorization;
        }

        [HttpPost("add-review")]
        [Authorize]
        public ActionResult AddReview([FromForm] NewReview newReview)
        {
            int reviewerID = tokenAuthorization.GetCurrentUser(User.Claims);

            Offer acceptedOffer = auctionsDBContext.Offers
                .Where(offer => offer.Item.OwnerID == newReview.UserID && offer.UserID == reviewerID && offer.isAccepted)
                .FirstOrDefault();

            if (acceptedOffer == null)
                return BadRequest("");

            DataLayer.Models.Review userReview = new DataLayer.Models.Review()
            {
                ReviewerID = reviewerID,
                UserID = newReview.UserID,
                Comment = newReview.Comment,
                Grade = newReview.Grade,
                ReviewDate = DateTime.Now
            };

            auctionsDBContext.UserReviews.Add(userReview);
            auctionsDBContext.SaveChanges();

            ReviewDetails review = auctionsDBContext.UserReviews
             .Where(review => review.ReviewerID == userReview.ReviewerID)
             .Select(review => new ReviewDetails
             {
                 Name = review.Reviewer.Name,
                 Lastname = review.Reviewer.Lastname,
                 Grade = review.Grade,
                 Comment = review.Comment,
                 ReviewDate = review.ReviewDate
             }).FirstOrDefault();

            return Ok(review);
        }

        [HttpGet("get-reviews/{userID}")]
        public ActionResult GetUserReviews(int userID)
        {
            List<ReviewDetails> userReviews = auctionsDBContext.UserReviews
                .Where(review => review.UserID == userID)
                .Select(review=>new ReviewDetails
                {
                    Name = review.Reviewer.Name,
                    Lastname = review.Reviewer.Lastname,
                    Grade = review.Grade,
                    Comment = review.Comment,
                    ReviewDate = review.ReviewDate
                })
                .ToList();

            return Ok(userReviews);
        }

    }
}
