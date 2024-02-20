using Application.Shared.Models;

namespace Application.Features.GetPaymentDetails.Models
{
    public record PaymentsResult(Guid TransactionId, string Status, string CardNumber, DateTimeOffset Date, float Amount, string Currency)
        : PaymentDetails(TransactionId, Status, CardNumber, Date, Amount, Currency);
    
}
