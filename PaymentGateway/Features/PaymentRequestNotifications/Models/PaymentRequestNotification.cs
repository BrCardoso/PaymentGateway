using Application.Shared.Models;
using MediatR;

namespace Application.Features.PaymentRequestNotifications.Models
{
    public record PaymentRequestNotification(Guid TransactionId, string Status, string CardNumber, DateTimeOffset Date, float Amount, string Currency)
        : PaymentDetails(TransactionId, Status, CardNumber, Date, Amount, Currency), INotification;
}
