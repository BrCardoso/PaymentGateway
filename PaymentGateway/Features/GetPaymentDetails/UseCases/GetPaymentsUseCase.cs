using Application.Features.GetPaymentDetails.Models;
using Application.Shared.Models;
using Application.Shared.Repository;
using MediatR;

namespace Application.Features.ListPayments.UseCases
{
    public class GetPaymentsUseCase : IRequestHandler<GetPaymentsCommand, Result<PaymentsResult>>
    {
        private readonly IDataStorageService _storageService;

        public GetPaymentsUseCase(IDataStorageService storageService)
        {
            _storageService = storageService;
        }

        public Task<Result<PaymentsResult>> Handle(GetPaymentsCommand request, CancellationToken cancellationToken)
        {
            var result = _storageService.Get<PaymentsResult>(request.TransactionId);
            return Task.FromResult(new Result<PaymentsResult>(result));
        }
    }
}
