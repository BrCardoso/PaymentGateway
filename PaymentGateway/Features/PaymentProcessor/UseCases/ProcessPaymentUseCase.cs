using Application.Features.PaymentProcessor.Models;
using Application.Features.PaymentProcessor.Repository;
using Application.Features.PaymentRequestNotifications.Models;
using Application.Shared.Models;
using MediatR;

namespace Application.Features.PaymentProcessor.UseCases
{
    public class ProcessPaymentUseCase : IRequestHandler<ProcessPaymentCommand, Result<ProcessPaymentResult>>
    {
        private readonly IPaymentProcessorRepository _repository;
        private IMediator _mediator;

        public ProcessPaymentUseCase(IPaymentProcessorRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<Result<ProcessPaymentResult>> Handle(ProcessPaymentCommand request, CancellationToken cancellationToken)
        {
            var result = await _repository.ProcessPaymentAsync(request.CardNumber, request.ExpiryMonth, request.ExpiryYear, request.Cvv, request.Name, cancellationToken);
            if (result.IsSuccess)
            {
                var notification = new PaymentRequestNotification(result.Value!.TransactionId, result.Value.Status, request.CardNumber, DateTimeOffset.Now, request.Amount, request.Currency);
                await _mediator.Publish(notification, cancellationToken);
            }
            return result;
        }
    }
}
