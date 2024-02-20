using Application.Shared.Models;
using MediatR;

namespace Application.Features.GetPaymentDetails.Models
{
    public record GetPaymentsCommand(Guid TransactionId) 
        : IRequest<Result<PaymentsResult>>;
}
