using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsBankVirtualPOS.Application.DTOs.Auth
{
    public class LogInResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public string Token { get; set; }

        public string Email { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public Guid UserId { get; set; }
    }
}
