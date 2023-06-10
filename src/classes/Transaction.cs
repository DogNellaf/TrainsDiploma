using System.Runtime.Serialization;

namespace TrainsClasses
{
    [DataContract]
    public class Transaction: Model
    {
        [DataMember]
        public int UserId { get; }
        [DataMember]
        public float Value { get; }
        [DataMember]
        public bool IsComplited { get; }
        [DataMember]
        public DateTime PaymentTime { get; }
        [DataMember]
        public string PaymentType { get; }
        [DataMember]
        public int KassaId { get; }
        [DataMember]
        public string Comment { get; }

        public Transaction() : base(-1)
        {

        }

        public Transaction(object[] items) : base((int)items[0])
        {
            UserId = (int)items[1];
            Value = float.Parse(items[2].ToString());
            IsComplited = (bool)items[3];
            PaymentTime = DateTime.Parse(items[4].ToString());
            PaymentType = (string)items[5];
            KassaId = (int)items[6];
            Comment = (string)items[7];
        }


        public Transaction(int id, int userId, float value, bool isComplited, DateTime paymentTime, string paymentType, int kassaId, string comment) : base(id)
        {
            UserId = userId;
            Value = value;
            IsComplited = isComplited;
            PaymentTime = paymentTime;
            PaymentType = paymentType;
            KassaId = kassaId;
            Comment = comment;
        }
    }
}
