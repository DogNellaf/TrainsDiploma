namespace TrainsClasses
{
    public class Route:Model
    {
        public DateTime DepartureTime { get; }
        public string DepartureCity { get; }
        public int DurationInMinutes { get; }
        public string ArrivalCity { get; }
        public Route(int id, DateTime departureTime, string departureCity, int durationInMinutes, string arrivalCity): base(id)
        {
            DepartureTime = departureTime;
            DepartureCity = departureCity;
            DurationInMinutes = durationInMinutes;
            ArrivalCity = arrivalCity;
        }
    }
}
