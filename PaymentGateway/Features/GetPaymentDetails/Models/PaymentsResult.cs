namespace Application.Features.GetPaymentDetails.Models
{
    public class PaymentsResult
    {
        public PaymentsResult(Guid transactionId, string status, string cardNumber, DateTimeOffset date, float amount, string currency)
        {
            TransactionId = transactionId;
            Status = status;
            CardNumber = cardNumber;
            Date = date;
            Amount = amount;
            Currency = currency;
        }

        public Guid TransactionId { get; }
        public string Status { get; }
        public string CardNumber { get; set; }
        public DateTimeOffset Date { get; }
        public float Amount { get; }
        public string Currency { get; }
    }
}
