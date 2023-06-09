﻿using System.Runtime.Serialization;

namespace TrainsClasses
{
    [DataContract]
    public class Transaction: Model
    {
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public float Value { get; set; }
        [DataMember]
        public bool IsComplited { get; set; }
        [DataMember]
        public DateTime PaymentTime { get; set; }
        [DataMember]
        public string PaymentType { get; set; }
        [DataMember]
        public string Comment { get; set; }

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
            Comment = (string)items[6];
        }


        public Transaction(int id, int userId, float value, bool isComplited, DateTime paymentTime, string paymentType, string comment) : base(id)
        {
            UserId = userId;
            Value = value;
            IsComplited = isComplited;
            PaymentTime = paymentTime;
            PaymentType = paymentType;
            Comment = comment;
        }
    }
}
