using IsBankVirtualPOS.Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsBankVirtualPOS.Application.Interfaces
{
    public interface IIdentityService
    {
        Task<(bool Success, string Message)> RegisterAsync(RegisterDTO registerDTO);
        Task<LogInResponseDTO> LogInAsync(LogInDTO logInDTO);
    }
}
