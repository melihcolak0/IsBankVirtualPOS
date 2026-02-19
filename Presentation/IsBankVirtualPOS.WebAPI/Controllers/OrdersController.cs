using IsBankVirtualPOS.Application.Features.Orders.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IsBankVirtualPOS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderCommand command)
        {
            var orderId = await _mediator.Send(command);

            return Ok(new
            {
                OrderId = orderId
            });
        }
    }
}
