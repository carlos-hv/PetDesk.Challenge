namespace PetDesk.Challenge.Models.Reports
{
    public class Distribution
    {
        public Distribution(string state)
        {
            State = state;
        }

        public string State { get; set; }
        public FlightCounter FlightCounter { get; set; }
    }

    public struct FlightCounter
    {
        public int InboundFlights { get; set; }
        public int OutboundFlights { get; set; }
    }
}