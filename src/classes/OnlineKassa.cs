namespace TrainsClasses
{
    public class OnlineKassa:Model
    {
        public string Name { get; }
        public PaymentType PaymentType { get; }

        public OnlineKassa(int id, string name, PaymentType paymentType): base(id)
        {
            Name = name;
            PaymentType = paymentType;
        }
    }
}
