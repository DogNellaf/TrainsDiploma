﻿using System.Runtime.Serialization;


namespace TrainsClasses
{
    [DataContract]
    public class Ticket : Model
    {
        [DataMember]
        public DateTime BuyTime { get; set; }
        [DataMember]
        public float Price { get; set; }
        [DataMember]
        public int RouteId { get; set; }
        [DataMember]
        public int StatusId { get; set; }
        [DataMember]
        public int UserId { get; set; }

        public Ticket() : base(-1)
        {

        }

        public Ticket(object[] items) : base((int)items[0])
        {
            BuyTime = DateTime.Parse(items[1].ToString());
            Price = float.Parse(items[2].ToString());
            RouteId = (int)items[3];
            StatusId = (int)items[4];
            UserId = (int)items[5];
        }

        public Ticket(int id, DateTime buyTime, float price, int routeId, int statusId, int userId) : base(id)
        {
            BuyTime = buyTime;
            Price = price;
            RouteId = routeId;
            StatusId = statusId;
            UserId = userId;
        }
    }
}
