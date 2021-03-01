using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using MoreLinq;
using PetDesk.Challenge.Models.DTO;
using PetDesk.Challenge.Services.Mapping;
using TinyCsvParser;

namespace PetDesk.Challenge.Services.ThirdParty
{
    public class FlightDataService
    {
        private readonly HttpClient _httpClient;

        public FlightDataService(string apiUrl)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(apiUrl)
            };
        }

        public async Task<IEnumerable<Airline>> GetAirlines()
        {
            var response = await _httpClient.GetStringAsync("airlines");
            var result = JsonSerializer.Deserialize<List<Airline>>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return result;
        }

        public async Task<IEnumerable<Airport>> GetAirports()
        {
            var response = await _httpClient.GetStringAsync("airports");
            var result = JsonSerializer.Deserialize<List<Airport>>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return result;
        }

        public async Task<IEnumerable<Flight>> GetAllFlights()
        {
            var result = new ConcurrentBag<Flight>();
            var monthTasks = Enumerable.Range(1, 12).Select(async n =>
                    await GetFlights(n)
                        .ContinueWith(taskResult =>
                            taskResult.Result.ForEach(f => result.Add(f))))
                .ToArray();
            await Task.WhenAll(monthTasks);
            return result.OrderByDescending(x => x.Month);
        }

        private async Task<IEnumerable<Flight>> GetFlights(int month)
        {
            var response = await _httpClient.GetStringAsync($"flights/{month}");
            var parser = new CsvParser<Flight>(new CsvParserOptions(true, ','), new CsvFlightMapping());
            var validator = new FlightValidator();
            var result = parser.ReadFromString(new CsvReaderOptions(new[] {Environment.NewLine}), response).ToList();
            var invalid = result.Where(x => !x.IsValid).ToList();
            foreach (var csvMappingResult in invalid)
                Debug.WriteLine(
                    $"error: {csvMappingResult.Error.Value}, unmapped: {csvMappingResult.Error.UnmappedRow}");
            return result
                .Where(x => x.IsValid)
                .Select(x => new {x.Result, ValidationResult = validator.Validate(x.Result)})
                .Where(x => x.ValidationResult.IsValid)
                .Select(x => x.Result);
        }
    }
}