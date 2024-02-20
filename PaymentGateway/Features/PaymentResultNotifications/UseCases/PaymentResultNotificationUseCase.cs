using Application.Features.PaymentResultNotifications.Models;
using Application.Shared.Models;
using Application.Shared.Repository;
using MediatR;
using Newtonsoft.Json;

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
            var a = await _storageService.Get<string>(notification.TransactionId);
            var cachedTransaction = JsonConvert.DeserializeObject<PaymentDetails>(a);
            var updatedTransaction = new PaymentDetails(notification.TransactionId, notification.Status, cachedTransaction.CardNumber, cachedTransaction.Date, cachedTransaction.Amount, cachedTransaction.Currency);
            await _storageService.Set(notification.TransactionId, updatedTransaction);
            return;
        }
    }
}
