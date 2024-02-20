using Application.Features.PaymentProcessor.Models;
using Application.Features.PaymentRequestNotifications.Models;
using Application.Features.PaymentResultNotifications.Models;
using Application.Features.PaymentResultNotifications.UseCases;
using Application.Shared.Models;
using Application.Shared.Repository;
using FluentAssertions;
using Moq;

namespace PaymentGateway.Tests.Features
{
    internal class PaymentResultNotificationUseCaseTests
    {
        private PaymentResultNotificationUseCase _paymentNotifcationUseCase;
        private readonly Mock<IDataStorageService> _storage = new();
        private CancellationToken _cancellationToken = new CancellationToken();

        [SetUp]
        public void Setup()
        {
            _paymentNotifcationUseCase = new PaymentResultNotificationUseCase(_storage.Object);
        }

        [TearDown]
        public void Teardown()
        {
            _storage.Verify();

            _storage.VerifyNoOtherCalls();
        }

        [Test]
        public async Task When_PaymentResultNotificationUseCase_SuccessAsync()
        {
            //Arrange
            var id = Guid.NewGuid();
            var request = new PaymentResultNotification(id, "Completed", true);
            var storageResult = new PaymentDetails(id, "Completed", "9879453132", DateTimeOffset.Now, 59, "USD");

            _storage.Setup(x => x.Get<PaymentDetails>(id))
                .ReturnsAsync(storageResult)
                .Verifiable();

            _storage.Setup(x => x.Set(id, It.IsAny<PaymentDetails>()))
                .ReturnsAsync(true)
                .Verifiable();

            //Act
            await _paymentNotifcationUseCase.Handle(request, _cancellationToken);

            //Assert
        }
    }
}
