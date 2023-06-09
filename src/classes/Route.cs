using System.Runtime.Serialization;

namespace TrainsClasses
{
    [DataContract]
    public class Route:Model
    {
        [DataMember]
        public DateTime DepartureTime { get; set; }
        [DataMember]
        public int DepartureCityId { get; set; }
        [DataMember]
        public int Duration { get; set; }
        [DataMember]
        public int ArrivalCityId { get; set; }
        [DataMember]
        public float Price { get; set; }

        public Route(): base(0)
        {

        }

        public Route(object[] items) : base((int)items[0])
        {
            DepartureTime = DateTime.Parse(items[1].ToString());
            DepartureCityId = (int)items[2];
            Duration = (int)items[3];
            ArrivalCityId = (int)items[4];
            Price = float.Parse(items[5].ToString());
        }

        public Route(int id, DateTime departureTime, int departureCityId, int duration, int arrivalCityId, float price): base(id)
        {
            DepartureTime = departureTime;
            DepartureCityId = departureCityId;
            Duration = duration;
            ArrivalCityId = arrivalCityId;
            Price = price;
        }
    }
}
