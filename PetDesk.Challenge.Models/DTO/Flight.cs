namespace PetDesk.Challenge.Models.DTO
{
    public class Flight
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public string FlightNumber { get; set; }
        public string Airline { get; set; }
        public string OriginAirport { get; set; }
        public string DestinationAirport { get; set; }
        public int DepartureDelay { get; set; }
        public int ArrivalDelay { get; set; }
        public bool Cancelled { get; set; }
    }
}