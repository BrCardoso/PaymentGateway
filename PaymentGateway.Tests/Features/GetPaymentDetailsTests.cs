using Application.Features.GetPaymentDetails.Models;
using Application.Features.ListPayments.UseCases;
using Application.Shared.Repository;
using FluentAssertions;
using Moq;

namespace PaymentGateway.Tests.Features
{
    internal class GetPaymentsUseCaseTests
    {
        private GetPaymentsUseCase _getPaymentsUseCase;
        private readonly Mock<IDataStorageService> _storageServiceMock = new();
        private CancellationToken _cancellationToken = new CancellationToken();

        [SetUp]
        public void Setup()
        {
            _getPaymentsUseCase = new GetPaymentsUseCase(_storageServiceMock.Object);
        }

        [TearDown] public void Teardown()
        {
            _storageServiceMock.Verify();

            _storageServiceMock.VerifyNoOtherCalls();
        }

        [Test]
        public async Task When_GetPaymentsUseCase_SuccessAsync()
        {
            //Arrange
            var id = Guid.NewGuid();
            var request = new GetPaymentsCommand(id);
            var storageResult = new PaymentsResult(id, "Completed", "1234567890", DateTimeOffset.Now, 50, "USD");

            _storageServiceMock.Setup(x => x.Get<PaymentsResult>(id))
                .ReturnsAsync(storageResult)
                .Verifiable();

            //Act
            var result = await _getPaymentsUseCase.Handle(request, _cancellationToken);

            //Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(storageResult);
            
        }

        [Test]
        public async Task When_GetPaymentsUseCase_ShouldBeErrorAsync()
        {
            //Arrange
            var id = Guid.NewGuid();
            var request = new GetPaymentsCommand(id);
            var storageResult = new PaymentsResult(id, "Completed", "1234567890", DateTimeOffset.Now, 50, "USD");

            _storageServiceMock.Setup(x => x.Get<PaymentsResult>(id))
                .Throws<Exception>()
                .Verifiable();

            //Act
            var result = await _getPaymentsUseCase.Handle(request, _cancellationToken);

            //Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Value.Should().BeNull();

        }
    }
}
