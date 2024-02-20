using Application.Features.PaymentProcessor.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace PaymentGateway.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentsController : Controller
    {
        private readonly IMediator _mediator;

        public PaymentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("List/{id}")]
        public ActionResult List(int id)
        {
            return View();
        }

        [HttpPost("Process")]
        public async Task<ActionResult> ProcessAsync([FromBody]ProcessPaymentCommand request, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(request, cancellationToken));
        }
    }
}
