using System.Collections.Generic;
using System.Threading.Tasks;
using PetDesk.Challenge.Caching.Providers;
using PetDesk.Challenge.Models.DTO;
using PetDesk.Challenge.Services.ThirdParty;

namespace PetDesk.Challenge.Caching
{
    public class FlightDataCache
    {
        private readonly FlightDataService _flightDataService;

        private readonly Redis _redis;

        private IEnumerable<Airline> Airlines;

        private IEnumerable<Airport> Airports;

        private IEnumerable<Flight> Flights;

        public bool IsInitialized;

        public FlightDataCache(FlightDataService fds, Redis redis)
        {
            _flightDataService = fds;
            _redis = redis;
            Task.Run(InitializeAsync).Wait();
        }

        private async Task InitializeAsync()
        {
            Airports ??= await _redis.GetAsync<IEnumerable<Airport>>("airports") ??
                         await _flightDataService.GetAirports();

            Airlines ??= await _redis.GetAsync<IEnumerable<Airline>>("airlines") ??
                         await _flightDataService.GetAirlines();

            Flights ??= await _redis.GetAsync<IEnumerable<Flight>>("flights") ??
                        await _flightDataService.GetAllFlights();

            IsInitialized = true;
        }
    }
}