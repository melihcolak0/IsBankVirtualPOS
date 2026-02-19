using IsBankVirtualPOS.Application.Features.Auth.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace IsBankVirtualPOS.WebUI.Controllers
{
    [Route("auth")]
    public class AuthController : Controller
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success)
                ModelState.AddModelError("", result.Message);

            return RedirectToAction("LogIn", "Auth");
        }

        [HttpGet("login")]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> LogIn(LogInUserCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success)
            {
                ModelState.AddModelError("", result.Message);
                return View(command);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost("logout")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            await _mediator.Send(new LogOutUserCommand());

            return RedirectToAction("LogIn", "Auth");
        }

        [HttpGet("isbank-callback")]
        public IActionResult IsBankCallback(string a)
        {
            return View();
        }
    }
}
