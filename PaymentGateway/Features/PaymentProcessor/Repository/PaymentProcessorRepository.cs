using Application.Features.GetPaymentDetails.Models;
using Application.Features.PaymentProcessor.Models;
using Application.Shared.AcquiringBank;
using Application.Shared.Models;
using Newtonsoft.Json;
using System.Text;

namespace Application.Features.PaymentProcessor.Repository
{
    public class PaymentProcessorRepository : IPaymentProcessorRepository
    {
        private readonly HttpClient _httpClient;
        private readonly IAcquiringBankAuth _acquiringBankApi;
        public PaymentProcessorRepository(IAcquiringBankAuth acquiringBankApi, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _acquiringBankApi = acquiringBankApi;
        }

        public async Task<Result<ProcessPaymentResult>> ProcessPaymentAsync(string cardNumber, int expMonth, int expYear, string cvvCode, string name, CancellationToken cancellationToken)
        {
            var token = await _acquiringBankApi.GetAuthorizationTokenAsync(cancellationToken);

            var data = new { CardNumber = cardNumber, ExpMonth = expMonth, ExpYear = expYear, CvvCode = cvvCode, Name = name };
            var json = JsonConvert.SerializeObject(data);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var response = await _httpClient.PostAsync("/Transactions/Payment", stringContent, cancellationToken);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    return new Result<ProcessPaymentResult>(JsonConvert.DeserializeObject<ProcessPaymentResult>(result));
                }
                return new Result<ProcessPaymentResult>("StatusCode", response.StatusCode.ToString());
            }
            catch (Exception e)
            {
                return new Result<ProcessPaymentResult>("Error while trying to call external service", e.ToString());
            }
        }
    }
}
