using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsBankVirtualPOS.Application.DTOs.Auth
{
    public class RegisterDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public bool IsMerchant { get; set; }        
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
