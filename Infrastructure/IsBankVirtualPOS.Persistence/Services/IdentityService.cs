using IsBankVirtualPOS.Application.DTOs.Auth;
using IsBankVirtualPOS.Application.Interfaces;
using IsBankVirtualPOS.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace IsBankVirtualPOS.Persistence.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtTokenService _jwtService;

        public IdentityService(UserManager<AppUser> userManager, IJwtTokenService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        public async Task<(bool Success, string Message)> RegisterAsync(RegisterDTO registerDTO)
        {
            var user = new AppUser
            {
                Name = registerDTO.Name,
                Surname = registerDTO.Surname,
                Email = registerDTO.Email,
                UserName = registerDTO.Username,
                City = registerDTO.City,
                IsMerchant = registerDTO.IsMerchant
            };

            var result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (!result.Succeeded)
            {
                return (false,
                    string.Join(",", result.Errors.Select(x => x.Description)));
            }

            return (true, "Kayýt baþarýlý");
        }

        public async Task<LogInResponseDTO> LogInAsync(LogInDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
                return new LogInResponseDTO
                {
                    Success = false,
                    Message = "Kullanýcý bulunamadý"
                };

            var valid = await _userManager.CheckPasswordAsync(user, dto.Password);

            if (!valid)
                return new LogInResponseDTO
                {
                    Success = false,
                    Message = "Þifre hatalý"
                };

            var token = await _jwtService.CreateTokenAsync(user);

            return new LogInResponseDTO
            {
                Success = true,
                Token = token,
                Email = user.Email,
                Name = user.Name,
                Role = "User",
                UserId = user.Id
            };
        }        
    }
}
