using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PetDesk.Challenge.Models.Reports;
using PetDesk.Challenge.Services;

namespace PetDesk.Challenge.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly ReportingService _reportingService;

        public ReportsController(ReportingService reportingService)
        {
            _reportingService = reportingService;
        }

        [HttpGet]
        [Route("")]
        public string Index()
        {
            return "Welcome to reports API!";
        }

        [HttpGet]
        [Route("distribution")]
        public async Task<IEnumerable<Distribution>> Distribution()
        {
            return await _reportingService.GetDistributionReportAsync();
        }

        [HttpGet]
        [Route("performance")]
        public async Task<IEnumerable<Performance>> Performance()
        {
            return await _reportingService.GetPerformanceReportAsync();
        }

        [HttpGet]
        [Route("recommendations")]
        public async Task<IEnumerable<Recommendation>> Recommendations([FromQuery] int number = 3)
        {
            return await _reportingService.GetRecommendationReportAsync(number);
        }
    }
}