using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetDesk.Challenge.Models.Reports;
using PetDesk.Challenge.Services.Caching;

namespace PetDesk.Challenge.Services.Reports
{
    public class RecommendationReport
    {
        public static async Task<IEnumerable<Recommendation>> Get(FlightDataCache cache, int number)
        {
            return await Task.FromResult(cache.Flights.GroupBy(x => x.OriginAirport).Select(x => new Recommendation
                {
                    AirportCode = x.Key,
                    Airport = cache.Airports.Single(a => a.Code == x.Key).Name,
                    CancelledFlights = x.Count(f => f.Cancelled),
                    DepartureDelayAverage = x.Average(f => f.DepartureDelay),
                    TotalOutboutFlights = x.Count()
                })
                .OrderByDescending(x => x.TotalOutboutFlights)
                .ThenBy(x => x.DepartureDelayAverage)
                .ThenBy(x => x.CancelledFlights)
                .Take(number));
        }
    }
}