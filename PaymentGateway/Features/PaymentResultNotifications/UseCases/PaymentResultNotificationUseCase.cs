using Application.Features.PaymentResultNotifications.Models;
using Application.Shared.Models;
using Application.Shared.Repository;
using MediatR;

namespace Application.Features.PaymentResultNotifications.UseCases
{
    public class PaymentResultNotificationUseCase : INotificationHandler<PaymentResultNotification>
    {
        private readonly IDataStorageService _storageService;

        public PaymentResultNotificationUseCase(IDataStorageService storageService)
        {
            _storageService = storageService;
        }

        public async Task Handle(PaymentResultNotification notification, CancellationToken cancellationToken)
        {
            var cachedTransaction = await _storageService.Get<PaymentDetails>(notification.TransactionId);
            var updatedTransaction = new PaymentDetails(notification.TransactionId, notification.Status, cachedTransaction.CardNumber, cachedTransaction.Date, cachedTransaction.Amount, cachedTransaction.Currency);
            await _storageService.Set(notification.TransactionId, updatedTransaction);
            return;
        }
    }
}
