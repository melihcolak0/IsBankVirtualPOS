using IsBankVirtualPOS.Application.Features.Refunds.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IsBankVirtualPOS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefundsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RefundsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("refund")]
        public async Task<IActionResult> Refund(CreateRefundCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(new { refundId = id });
        }
    }
}
