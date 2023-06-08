using System.Runtime.Serialization;

namespace TrainsClasses
{
    [DataContract]
    public class Role : Model
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public bool IsWorker { get; set; }
        [DataMember]
        public bool IsAdmin { get; set; }

        public Role() : base(0)
        {

        }

        public Role(object[] items) : base((int)items[0])
        {
            Name = (string)items[1];
            IsWorker = (bool)items[2];
            IsAdmin = (bool)items[3];
        }

        public Role(int id, string name, bool isWorker, bool isAdmin) : base(id)
        {
            Name = name;
            IsWorker = isWorker;
            IsAdmin = isAdmin;
        }
    }
}
