using System;
using FluentValidation;
using PetDesk.Challenge.Models.DTO;
using TinyCsvParser.Mapping;
using TinyCsvParser.TypeConverter;

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
            MapProperty(11, x => x.DepartureDelay, new CustomNumberConverter());
            MapProperty(22, x => x.ArrivalDelay, new CustomNumberConverter());
            MapProperty(24, x => x.Cancelled, new BoolConverter("True", "False", StringComparison.Ordinal));
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

    internal class CustomNumberConverter : BaseConverter<int>
    {
        public override bool TryConvert(string value, out int result)
        {
            if (string.IsNullOrEmpty(value))
            {
                result = 0;
                return true;
            }

            return int.TryParse(value, out result);
        }
    }
}