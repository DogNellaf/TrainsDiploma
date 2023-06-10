using TrainsClasses;

namespace API.Services
{
    public class TransactionService : Service<Transaction>
    {
        public override string GetUpdateValues(Transaction data)
        {
            int isComplited = GetComplited(data);

            return $"UserId = {data.UserId}, " +
                   $"Value = {data.Value}, " +
                   $"IsComplited = {isComplited}, " +
                   $"PaymentTime = '{data.PaymentTime:yyyy-dd-MM HH:mm:ss.fff}', " +
                   $"PaymentType = '{data.PaymentType}',  " +
                   $"Comment = '{data.Comment}'";
        }

        public override string GetCreateValues(Transaction data)
        {
            int isComplited = GetComplited(data);

            return $"(UserId, " +
                   $"Value, " +
                   $"IsComplited, " +
                   $"PaymentTime, " +
                   $"PaymentType, " +
                   $"Comment) " +
                   $"VALUES (" +
                   $"{data.UserId}, " +
                   $"{data.Value}, " +
                   $"{isComplited}, " +
                   $"'{data.PaymentTime:yyyy-dd-MM HH:mm:ss.fff}', " +
                   $"'{data.PaymentType}', " +
                   $"'{data.Comment}')";
        }

        private int GetComplited(Transaction data)
        {
            int isComplited = 1;
            if (!data.IsComplited)
            {
                isComplited = 0;
            }
            return isComplited;
        }
    }
}
