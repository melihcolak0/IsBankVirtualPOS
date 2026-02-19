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
    public class LogInUserCommandHandler : IRequestHandler<LogInUserCommand, LogInResponseDTO>
    {
        private readonly IIdentityService _identityService;

        public LogInUserCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<LogInResponseDTO> Handle(
            LogInUserCommand request,
            CancellationToken cancellationToken)
        {
            return await _identityService.LogInAsync(new LogInDTO
            {
                Email = request.Email,
                Password = request.Password,
                RememberMe = request.RememberMe
            });
        }
    }
}
