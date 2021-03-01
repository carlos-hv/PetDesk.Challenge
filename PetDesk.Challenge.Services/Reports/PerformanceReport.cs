using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetDesk.Challenge.Models.Reports;
using PetDesk.Challenge.Services.Caching;

namespace PetDesk.Challenge.Services.Reports
{
    public class PerformanceReport
    {
        public static async Task<IEnumerable<Performance>> GetAsync(FlightDataCache cache)
        {
            var airlineGroups = cache.Flights.GroupBy(x => x.Airline).ToList();
            var airlineAverages = new Dictionary<string, Tuple<int, double>>();
            foreach (var flightsByAirline in airlineGroups)
                airlineAverages[flightsByAirline.Key] = new Tuple<int, double>(flightsByAirline.Count(),
                    flightsByAirline.Average(x => x.ArrivalDelay));

            return await Task.FromResult(
                airlineAverages
                    .OrderByDescending(x => x.Value.Item1)
                    .ThenBy(x => x.Value.Item2)
                    .Select(x =>
                        new Performance
                        {
                            Airline = cache.Airlines.Single(a => a.Code == x.Key).Name,
                            AirlineCode = x.Key,
                            TotalFlights = x.Value.Item1,
                            ArrivalTimeDeltaAverage = x.Value.Item2
                        }));
        }
    }
}