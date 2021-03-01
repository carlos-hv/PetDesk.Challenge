using System.Collections.Generic;
using System.Threading.Tasks;
using PetDesk.Challenge.Models.DTO;
using PetDesk.Challenge.Services.Caching.Providers;
using PetDesk.Challenge.Services.ThirdParty;

namespace PetDesk.Challenge.Services.Caching
{
    public class FlightDataCache
    {
        private readonly FlightDataService _flightDataService;

        private readonly Redis _redis;

        public IEnumerable<Airline> Airlines;

        public IEnumerable<Airport> Airports;

        public IEnumerable<Flight> Flights;

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

            if (!await _redis.ExistsAsync("airlines"))
                await _redis.AddAsync("airlines", Airlines);
            if (!await _redis.ExistsAsync("airports"))
                await _redis.AddAsync("airports", Airports);
            if (!await _redis.ExistsAsync("flights"))
                await _redis.AddAsync("flights", Flights);

            IsInitialized = true;
        }
    }
}