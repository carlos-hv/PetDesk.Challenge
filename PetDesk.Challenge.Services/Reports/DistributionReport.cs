using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoreLinq;
using PetDesk.Challenge.Models.Reports;
using PetDesk.Challenge.Services.Caching;

namespace PetDesk.Challenge.Services.Reports
{
    public class DistributionReport
    {
        public static async Task<IEnumerable<Distribution>> GetAsync(FlightDataCache cache)
        {
            var states = cache.Airports.GroupBy(x => x.State).Select(x => x.Key).ToList();
            var distributionByState = new ConcurrentDictionary<string, FlightCounter>(states.Select(x =>
                new KeyValuePair<string, FlightCounter>(x, new FlightCounter())));
            var batches = cache.Flights.Batch(10000);
            Parallel.ForEach(batches, batch =>
            {
                foreach (var flight in batch)
                {
                    var originState = cache.Airports.Single(x => x.Code == flight.OriginAirport).State;
                    var originCounter = distributionByState[originState];
                    var destinationState = cache.Airports.Single(x => x.Code == flight.DestinationAirport).State;
                    var destinationCounter = distributionByState[destinationState];

                    originCounter.OutboundFlights++;
                    destinationCounter.InboundFlights++;
                    distributionByState[originState] = originCounter;
                    distributionByState[destinationState] = destinationCounter;
                }
            });

            return await Task.FromResult(distributionByState.Keys.Select(x => new Distribution(x)
                {
                    FlightCounter = distributionByState[x]
                })
                .OrderByDescending(x => x.FlightCounter.OutboundFlights)
                .ThenByDescending(x => x.FlightCounter.InboundFlights));
        }
    }
}