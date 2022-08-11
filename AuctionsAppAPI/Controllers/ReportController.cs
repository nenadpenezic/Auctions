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

        [HttpPost("report-user/{userID}")]
        [Authorize]
        public ActionResult ReportUser([FromBody] UserReportDTO report, int userID)
        {
            int currentUser = tokenAuthorization.GetCurrentUser(User.Claims);

            UserReport userReport = new UserReport()
            {
                UserReporterID = currentUser,
                ReportAgainstUserID = userID,
                ReportTitle = report.ReportTitle,
                ReportDetails = report.Report,
                DateTime = DateTime.Now
            };

            auctionsDBContext.UserReports.Add(userReport);
            if(auctionsDBContext.SaveChanges()>0)
                return Ok(report);

            return BadRequest();
        }

        [HttpGet ("get-reports")]
      
        public ActionResult GetReports()
        {
            List<UserReportDetails> userReportDetails = auctionsDBContext.UserReports.Select(userReport => new UserReportDetails
            {
                ReporterName = userReport.UserReporter.Name,
                ReporterLastname = userReport.UserReporter.Lastname,

                ReportAgainstName = userReport.ReportAgainstUser.Name,
                ReportAgainsLastname = userReport.ReportAgainstUser.Lastname,

                ReportTitle = userReport.ReportTitle,
                ReportDetails = userReport.ReportDetails,
           
            }).ToList();

            return Ok(userReportDetails);
        }
    }
}
