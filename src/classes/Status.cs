using System.Runtime.Serialization;

namespace TrainsClasses
{
    [DataContract]
    public class Status: Model
    {
        [DataMember]
        public string Name { get; set; }

        public Status() : base(-1)
        {

        }

        public Status(object[] items) : base((int)items[0])
        {
            Name = (string)items[1];
        }

        public Status(int id, string name) : base(id)
        {
            Name = name;
        }
    }
}
