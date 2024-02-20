using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace AcquiringBankSimulator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionsController : ControllerBase
    {
        [HttpPost("Payment")]
        public async Task<object> PostAsync(object request, CancellationToken cancellationToken)
        {
            var result = new
            {
                Success = true,
                TransactionId = Guid.NewGuid(),
                Status = "Received"
            };

            return result;
        }

        [HttpPost("Payment/UpdateStatus")]
        public async Task<object> PostStatusAsync([FromBody]Guid id, CancellationToken cancellationToken)
        {
            await ProcessResponse(id, cancellationToken);

            return Ok();
        }

        private async Task ProcessResponse(Guid transactionId, CancellationToken cancellationToken)
        {
            ///////////////////////////////////
            //Since this is just a simulator I'm not focusing on structuring this project, that's why the client is in here
            ///////////////////////////////////
            var status = new[] { "Completed", "Non Suficient Funds", "Blocked" };

            var selectedStatus = status[new Random().Next(0, 2)];
            HttpClient httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(
                new
                {
                    Success = true,
                    TransactionId = transactionId,
                    Status = selectedStatus

                });
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            await httpClient.PostAsync("https://localhost:7035/Payments/Update", stringContent, cancellationToken);
        }
    }
}
