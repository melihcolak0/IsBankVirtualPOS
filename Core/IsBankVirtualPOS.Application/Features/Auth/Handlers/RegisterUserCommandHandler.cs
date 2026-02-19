using IsBankVirtualPOS.Application.DTOs.Auth;
using IsBankVirtualPOS.Application.Features.Auth.Commands;
using IsBankVirtualPOS.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsBankVirtualPOS.Application.Features.Auth.Handlers
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterUserResponse>
    {
        private readonly IIdentityService _identityService;

        public RegisterUserCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<RegisterUserResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            if (request.Password != request.ConfirmPassword)
            {
                return new RegisterUserResponse
                {
                    Success = false,
                    Message = "Þifreler uyuþmuyor"
                };
            }

            var result = await _identityService.RegisterAsync(new RegisterDTO
            {
                Name = request.Name,
                Surname = request.Surname,
                Username = request.Username,
                Email = request.Email,
                City = request.City,
                IsMerchant = request.IsMerchant,
                Password = request.Password,
                ConfirmPassword = request.ConfirmPassword
            });              

            return new RegisterUserResponse
            {
                Success = result.Success,
                Message = result.Message
            };
        }
    }
}
