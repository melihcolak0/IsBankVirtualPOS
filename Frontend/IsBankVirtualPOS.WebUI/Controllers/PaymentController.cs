using IsBankVirtualPOS.WebUI.DTOs.PaymentDTOs;
using IsBankVirtualPOS.WebUI.Services.OrderServices;
using IsBankVirtualPOS.WebUI.Services.PaymentServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace IsBankVirtualPOS.WebUI.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        private readonly OrderService _orderService;
        private readonly PaymentService _paymentService;

        public PaymentController(OrderService orderService, PaymentService paymentService)
        {
            _orderService = orderService;
            _paymentService = paymentService;
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(decimal amount)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var orderId = await _orderService.CreateOrder(new()
            {
                AppUserId = Guid.Parse(userId!),
                Amount = amount,
                Currency = "TRY"
            });

            var paymentId = await _paymentService.CreatePaymentAsync(new()
            {
                OrderId = orderId!.Value,
                Amount = amount,
                Provider = 1
            });

            return RedirectToAction("Card", new { paymentId });
        }

        [HttpGet]
        public IActionResult Card(Guid paymentId)
        {
            return View(new StartPaymentDTO
            {
                PaymentId = paymentId
            });
        }

        [HttpPost]
        public async Task<IActionResult> StartPayment(StartPaymentDTO dto)
        {
            if (!dto.Use3D)
            {
                var html = await _paymentService.StartPayment(dto);

                return RedirectToAction("Result",
                    new { paymentId = dto.PaymentId });
            }

            var form = await _paymentService.Start3D(dto);

            if (form == null)
                return RedirectToAction("Result", new { paymentId = dto.PaymentId });

            var summary = await _paymentService.GetSummary(dto.PaymentId);

            if (summary != null)
            {
                form.Amount = summary.Amount;
            }

            return View("ThreeDPaymentPage", form);
        }        

        [HttpGet]
        public async Task<IActionResult> Result(Guid paymentId)
        {
            var summary = await _paymentService.GetSummary(paymentId);

            return View(summary);
        }

        [HttpPost]
        public async Task<IActionResult> Refund(Guid paymentId)
        {
            await _paymentService.Refund(paymentId);

            return RedirectToAction("Result", new { paymentId });
        }

        [HttpGet]
        public IActionResult ThreeDPaymentPage(ThreeDPaymentFormDTO dto)
        {
            return View(dto);
        }
    }
}
