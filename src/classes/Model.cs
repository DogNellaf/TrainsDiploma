using System.Runtime.Serialization;

namespace TrainsClasses
{
    [DataContract]
    public class Model
    {
        [DataMember]
        public int Id { get; set; }
        public Model(int id)
        {
            Id = id;
        }
    }
}
