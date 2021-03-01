using System.Security.Cryptography.X509Certificates;
using FluentValidation;
using PetDesk.Challenge.Models.DTO;
using TinyCsvParser.Mapping;

namespace PetDesk.Challenge.Services.Mapping
{
    internal class CsvFlightMapping : CsvMapping<Flight>
    {
        public CsvFlightMapping()
        {
            MapProperty(0, x => x.Year);
            MapProperty(1, x => x.Month);
            MapProperty(4, x => x.Airline);
            MapProperty(5, x => x.FlightNumber);
            MapProperty(7, x => x.OriginAirport);
            MapProperty(8, x => x.DestinationAirport);
            MapProperty(22, x => x.ArrivalDelay);
            MapProperty(23, x => x.Diverted);
            MapProperty(24, x => x.Cancelled);
        }
    }

    internal class FlightValidator : AbstractValidator<Flight>
    {
        public FlightValidator()
        {
            RuleFor(x => x.OriginAirport)
                .NotNull()
                .NotEmpty()
                .Length(3);

            RuleFor(x => x.DestinationAirport)
                .NotNull()
                .NotEmpty()
                .Length(3);
        }
    }
}