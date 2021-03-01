using System.Collections.Generic;
using System.Threading.Tasks;
using PetDesk.Challenge.Models.Reports;
using PetDesk.Challenge.Services.Caching;
using PetDesk.Challenge.Services.Reports;

namespace PetDesk.Challenge.Services
{
    public class ReportingService
    {
        private readonly FlightDataCache _cache;

        public ReportingService(FlightDataCache cache)
        {
            _cache = cache;
        }

        public async Task<IEnumerable<Distribution>> GetDistributionReportAsync()
        {
            return await DistributionReport.GetAsync(_cache);
        }

        public async Task<IEnumerable<Performance>> GetPerformanceReportAsync()
        {
            return await PerformanceReport.GetAsync(_cache);
        }
    }
}
