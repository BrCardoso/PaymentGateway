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

        public async Task<Result<PaymentsResult>> Handle(GetPaymentsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _storageService.Get<PaymentsResult>(request.TransactionId);
                result.CardNumber = Mask(result.CardNumber);
                return new Result<PaymentsResult>(result);
            }
            catch
            {
                return new Result<PaymentsResult>("Not found", "Error trying to retrieve transaction");
            }            
        }
        public static string Mask(string data)
        {
            var lastDigits = data.Substring(data.Length - 4, 4);
            var mask = new string('X', data.Length - 4);

            return string.Concat(mask, lastDigits);
        }
    }
}
