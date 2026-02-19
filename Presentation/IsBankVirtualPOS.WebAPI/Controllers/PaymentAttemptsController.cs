using IsBankVirtualPOS.Application.Features.PaymentAttempts.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IsBankVirtualPOS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentAttemptsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentAttemptsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePaymentAttemptCommand command)
        {
            var attemptId = await _mediator.Send(command);

            return Ok(new
            {
                PaymentAttemptId = attemptId
            });
        }
    }
}
