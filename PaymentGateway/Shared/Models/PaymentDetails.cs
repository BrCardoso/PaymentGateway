namespace Application.Shared.Models
{
    public record PaymentDetails(Guid TransactionId, string Status, string CardNumber, DateTimeOffset Date, float Amount, string Currency);
}
