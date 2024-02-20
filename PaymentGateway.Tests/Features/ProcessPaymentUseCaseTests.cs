using Application.Features.PaymentProcessor.Models;
using Application.Features.PaymentProcessor.Repository;
using Application.Features.PaymentProcessor.UseCases;
using Application.Features.PaymentRequestNotifications.Models;
using Application.Shared.Models;
using FluentAssertions;
using MediatR;
using Moq;

namespace PaymentGateway.Tests.Features
{
    internal class ProcessPaymentUseCaseTests
    {
        private ProcessPaymentUseCase _processPaymentUseCase;
        private readonly Mock<IPaymentProcessorRepository> _paymentProcessorRepository = new();
        private readonly Mock<IMediator> _mediator = new();
        private CancellationToken _cancellationToken = new CancellationToken();

        [SetUp]
        public void Setup()
        {
            _processPaymentUseCase = new ProcessPaymentUseCase(_paymentProcessorRepository.Object, _mediator.Object);
        }

        [TearDown]
        public void Teardown()
        {
            _paymentProcessorRepository.Verify();
            _mediator.Verify();

            _paymentProcessorRepository.VerifyNoOtherCalls();
            _mediator.VerifyNoOtherCalls();
        }

        [Test]
        public async Task When_ProcessPaymentUseCase_SuccessAsync()
        {
            //Arrange
            var id = Guid.NewGuid();
            var request = new ProcessPaymentCommand("9879453132", 10, 2027, 59, "USD", "321", "Bruna Silva");
            var storageResult = new ProcessPaymentResult(id, "Completed");

            _paymentProcessorRepository.Setup(x => x.ProcessPaymentAsync(request.CardNumber, request.ExpiryMonth, request.ExpiryYear, request.Amount, request.Currency, request.Cvv, request.Name, _cancellationToken))
                .ReturnsAsync(new Result<ProcessPaymentResult>(storageResult))
                .Verifiable();
            _mediator.Setup(x => x.Publish(It.IsAny<PaymentRequestNotification>(), _cancellationToken))
                .Verifiable();

            //Act
            var result = await _processPaymentUseCase.Handle(request, _cancellationToken);

            //Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(storageResult);
        }

        [Test]
        public async Task When_ProcessPaymentUseCase_ErrorAsync()
        {
            //Arrange
            var id = Guid.NewGuid();
            var request = new ProcessPaymentCommand("9879453132", 10, 2027, 59, "USD", "321", "Bruna Silva");
            var storageResult = new Result<ProcessPaymentResult>("Error", "Error message");

            _paymentProcessorRepository.Setup(x => x.ProcessPaymentAsync(request.CardNumber, request.ExpiryMonth, request.ExpiryYear, request.Amount, request.Currency, request.Cvv, request.Name, _cancellationToken))
                .ReturnsAsync(storageResult)
                .Verifiable();

            //Act
            var result = await _processPaymentUseCase.Handle(request, _cancellationToken);

            //Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.Value.Should().BeNull();
        }
    }
}
