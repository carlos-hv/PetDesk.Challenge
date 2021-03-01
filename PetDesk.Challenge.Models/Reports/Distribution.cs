namespace PetDesk.Challenge.Models.Reports
{
    public class Distribution
    {
        public Distribution(string state)
        {
            State = state;
        }

        public string State { get; set; }
        public int InboundFlights { get; set; }
        public int OutboundFlights { get; set; }
    }

    public class FlightCounter
    {
        public int InboundFlights { get; set; }
        public int OutboundFlights { get; set; }
    }
}