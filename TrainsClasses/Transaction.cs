namespace TrainsClasses
{
    public class Transaction:Model
    {
        public int UserId { get; }
        public float Value { get; }
        public bool IsComplited { get; }
        public DateTime PaymentDate { get; }
        public int KassaId { get; }
        public Transaction(int id, int userId, float value, bool isComplited, DateTime paymentDate, int kassaId) : base(id)
        {
            UserId = userId;
            Value = value;
            IsComplited = isComplited;
            PaymentDate = paymentDate;
            KassaId = kassaId;
        }
    }
}
