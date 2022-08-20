using AuctionsAppAPI.Authorization;
using AuctionsAppAPI.DTO;
using DataLayer.DatabaseConfiguration;
using DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuctionsAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private AuctionsDBContext auctionsDBContext;
        private ITokenAuthorization tokenAuthorization;
        public ReportController(
            AuctionsDBContext _auctionsDBContext,
            ITokenAuthorization _tokenAuthorization)
        {
            auctionsDBContext = _auctionsDBContext;
            tokenAuthorization = _tokenAuthorization;
        }

        [HttpPost("report-user")]
        [Authorize]
        public ActionResult AddReport([FromForm] NewReport report)
        {
            int currentUser = tokenAuthorization.GetCurrentUser(User.Claims);
           
            Report userReport = new Report()
            {
                UserReporterID = currentUser,
                ReportAgainstUserID = report.UserID,
                ReportTitle = report.Title,
                ReportDetails = report.Explanation,
                DateTime = DateTime.Now
            };

            auctionsDBContext.UserReports.Add(userReport);
            auctionsDBContext.SaveChanges();

            return Ok();
        }

        [HttpGet ("get-reports")]
        [Authorize]
        public ActionResult GetReports()
        {
            Claim role = User.Claims
                 .FirstOrDefault(claim => claim.Type.ToString()
                 .Equals("Role", StringComparison.InvariantCultureIgnoreCase));

            if (!String.Equals(role.Value, "Administrator"))
                return Unauthorized();

            List<UserReportDetails> userReportDetails = auctionsDBContext.UserReports
                .Select(userReport => new UserReportDetails
            {
                ReporterID = userReport.UserReporterID,
                ReporterName = userReport.UserReporter.Name,
                ReporterLastname = userReport.UserReporter.Lastname,

                ReportAgainstName = userReport.ReportAgainstUser.Name,
                ReportAgainsLastname = userReport.ReportAgainstUser.Lastname,

                ReportTitle = userReport.ReportTitle,
                ReportDetails = userReport.ReportDetails,
                
                ReportDate = userReport.DateTime
            }).ToList();

            return Ok(userReportDetails);
        }


        }
}
