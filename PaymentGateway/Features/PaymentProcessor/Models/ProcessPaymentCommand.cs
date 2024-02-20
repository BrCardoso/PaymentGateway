using Application.Shared.Models;
using MediatR;

namespace Application.Features.PaymentProcessor.Models
{
    public record ProcessPaymentCommand(string CardNumber, int ExpiryMonth, int ExpiryYear, float Amount, string Currency, string Cvv, string Name) 
        : IRequest<Result<ProcessPaymentResult>>;
}
