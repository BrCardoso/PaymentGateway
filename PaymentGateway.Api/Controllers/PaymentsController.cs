using Application.Features.GetPaymentDetails.Models;
using Application.Features.PaymentProcessor.Models;
using Application.Features.PaymentResultNotifications.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PaymentGateway.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetAsync(Guid id, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetPaymentsCommand(id), cancellationToken));
        }

        [HttpPost("Process")]
        public async Task<ActionResult> ProcessAsync([FromBody] ProcessPaymentCommand request, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(request, cancellationToken));
        }

        [HttpPost("Update")]
        public async Task<ActionResult> UpdateAsync([FromBody] PaymentResultNotification request, CancellationToken cancellationToken)
        {
            await _mediator.Publish(request, cancellationToken);
            return Ok();
        }
    }
}
