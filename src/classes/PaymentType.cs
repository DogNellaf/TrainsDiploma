using System.Runtime.Serialization;

namespace TrainsClasses
{
    [DataContract]
    public class PaymentType : Model
    {
        [DataMember]
        public string Name { get; set; }

        public PaymentType() : base(-1)
        {

        }

        public PaymentType(object[] items) : base((int)items[0])
        {
            Name = (string)items[1];
        }

        public PaymentType(int id, string name) : base(id)
        {
            Name = name;
        }
    }
}
