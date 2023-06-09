using System.Runtime.Serialization;

namespace TrainsClasses
{
    [DataContract]
    public class City : Model
    {
        [DataMember]
        public string Name { get; set; }

        public City() : base(0)
        {

        }

        public City(object[] items) : base((int)items[0])
        {
            Name = (string)items[1];
        }

        public City(int id, string name) : base(id)
        {
            Name = name;
        }
    }
}
