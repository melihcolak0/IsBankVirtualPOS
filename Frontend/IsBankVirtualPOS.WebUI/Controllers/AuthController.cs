using IsBankVirtualPOS.WebUI.DTOs.AuthDTOs;
using IsBankVirtualPOS.WebUI.Services.AuthServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IsBankVirtualPOS.WebUI.Controllers
{
    [Route("auth")]
    public class AuthController : Controller
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var success = await _authService.RegisterAsync(dto);

            if (!success)
            {
                ModelState.AddModelError("", "Kayıt başarısız!");
                return View(dto);
            }

            return RedirectToAction("LogIn");
        }

        [HttpGet("login")]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> LogIn(LogInDTO dto)
        {
            var result = await _authService.LoginAsync(dto);

            if (result == null || !result.Success)
            {
                ModelState.AddModelError("", "Giriş başarısız!");
                return View(dto);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, result.Name ?? ""),
                new Claim(ClaimTypes.Email, result.Email ?? ""),
                new Claim(ClaimTypes.Role, result.Role ?? "User"),
                new Claim(ClaimTypes.NameIdentifier, result.UserId.ToString()),
                new Claim("AccessToken", result.Token)
            };

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = true
                });

            return RedirectToAction("Checkout", "Payment");
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("LogIn");
        }
    }
}
