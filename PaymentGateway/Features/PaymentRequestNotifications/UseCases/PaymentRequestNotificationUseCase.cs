using Application.Features.PaymentRequestNotifications.Models;
using Application.Shared.Repository;
using MediatR;
using Newtonsoft.Json;

namespace Application.Features.PaymentRequestNotifications.UseCases
{
    public class PaymentRequestNotificationUseCase : INotificationHandler<PaymentRequestNotification>
    {
        private readonly IDataStorageService _storageService;

        public PaymentRequestNotificationUseCase(IDataStorageService storageService)
        {
            _storageService = storageService;
        }

        public Task Handle(PaymentRequestNotification notification, CancellationToken cancellationToken)
        {
            _storageService.Set(notification.TransactionId, JsonConvert.SerializeObject(notification));
            return Task.CompletedTask;
        }
    }
}
