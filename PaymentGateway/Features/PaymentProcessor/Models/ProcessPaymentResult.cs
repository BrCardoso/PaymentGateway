namespace Application.Features.PaymentProcessor.Models
{
    public record ProcessPaymentResult(Guid TransactionId, string Status);
}
