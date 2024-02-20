using Application.Shared.Models;
using MediatR;

namespace Application.Features.PaymentResultNotifications.Models
{
    public record PaymentResultNotification(Guid TransactionId, string Status, bool Successs) : INotification;
}
