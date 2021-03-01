namespace PetDesk.Challenge.Models.Reports
{
    public class Recommendation
    {
        public string Airport { get; set; }
        public string AirportCode { get; set; }
        public int TotalOutboutFlights { get; set; }
        public double CancelledFlights { get; set; }
        public double DepartureDelayAverage { get; set; }
    }
}