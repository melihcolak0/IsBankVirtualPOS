using IsBankVirtualPOS.Application.DTOs.Payment;
using IsBankVirtualPOS.Application.Features.Payments.Commands;
using IsBankVirtualPOS.Application.Features.Payments.Queries;
using IsBankVirtualPOS.Application.Interfaces.PaymentInterfaces;
using IsBankVirtualPOS.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IsBankVirtualPOS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IPaymentRepository _paymentRepository;

        public PaymentsController(IMediator mediator, IPaymentRepository paymentRepository)
        {
            _mediator = mediator;
            _paymentRepository = paymentRepository;
        }

        [HttpGet("{paymentId}/summary")]
        public async Task<IActionResult> GetSummary(Guid paymentId)
        {
            var result = await _mediator.Send(
                new GetPaymentSummaryQuery(paymentId));

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePaymentCommand command)
        {
            var paymentId = await _mediator.Send(command);

            return Ok(new { PaymentId = paymentId });
        }

        [HttpPost("start")]
        public async Task<IActionResult> Start(StartPaymentCommand command)
        {
            var result = await _mediator.Send(command);

            if (result == null)
                return Ok();

            return Content(result, "text/html");
        }

        [HttpPost("start-3d")]
        public async Task<IActionResult> Start3D(Start3DPaymentCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("3d-callback")]
        public async Task<IActionResult> ThreeDCallback([FromForm] ThreeDCallbackRequest request)
        {
            await _mediator.Send(new CompleteThreeDPaymentCommand
            {
                PaymentId = request.PaymentId,
                MdStatus = request.MdStatus,
                ResponseCode = request.ResponseCode,
                AuthCode = request.AuthCode,
                BankReferenceNumber = request.BankReferenceNumber
            });

            return Redirect($"https://localhost:7149/Payment/Result?paymentId={request.PaymentId}");
        }

        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp(VerifyOtpCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result)
                return BadRequest("OTP invalid");

            return Ok();
        }
    }
}
