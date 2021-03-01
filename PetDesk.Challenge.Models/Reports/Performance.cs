namespace PetDesk.Challenge.Models.Reports
{
    public class Performance
    {
        public string Airline { get; set; }
        public string AirlineCode { get; set; }
        public int TotalFlights { get; set; }
        public double ArrivalTimeDeltaAverage { get; set; }
    }
}
