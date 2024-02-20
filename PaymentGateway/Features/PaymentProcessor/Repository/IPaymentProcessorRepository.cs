using Application.Features.PaymentProcessor.Models;
using Application.Shared.Models;

namespace Application.Features.PaymentProcessor.Repository
{
    public interface IPaymentProcessorRepository
    {
        Task<Result<ProcessPaymentResult>> ProcessPaymentAsync(string cardNumber, int expMonth, int expYear, float amount, string currency, string cvvCode, string name, CancellationToken cancellationToken);
    }
}