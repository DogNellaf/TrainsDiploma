using System.Runtime.Serialization;

namespace TrainsClasses
{
    [DataContract]
    public class Route:Model
    {
        [DataMember]
        public DateTime DepartureTime { get; set; }
        [DataMember]
        public string DepartureCity { get; set; }
        [DataMember]
        public int DurationInMinutes { get; set; }
        [DataMember]
        public string ArrivalCity { get; set; }

        public Route(): base(0)
        {

        }

        public Route(object[] items) : base((int)items[0])
        {
            DepartureTime = DateTime.Parse(items[1].ToString());
            DepartureCity = (string)items[2];
            DurationInMinutes = (int)items[3];
            ArrivalCity = (string)items[4];
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
