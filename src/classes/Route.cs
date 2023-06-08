namespace TrainsClasses
{
    public class Route:Model
    {
        public DateTime DepartureTime { get; }
        public string DepartureCity { get; }
        public int DurationInMinutes { get; }
        public string ArrivalCity { get; }

        public Route(object[] items) : base((int)items[0])
        {
            DepartureTime = DateTime.Parse((string)items[1]);
            DepartureCity = (string)items[2];
            DurationInMinutes = (int)items[4];
            ArrivalCity = (string)items[3];
        }

        public Route(int id, DateTime departureTime, string departureCity, int durationInMinutes, string arrivalCity): base(id)
        {
            DepartureTime = departureTime;
            DepartureCity = departureCity;
            DurationInMinutes = durationInMinutes;
            ArrivalCity = arrivalCity;
        }
    }
}
