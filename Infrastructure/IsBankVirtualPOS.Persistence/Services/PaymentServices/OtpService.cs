using IsBankVirtualPOS.Application.Interfaces.PaymentInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsBankVirtualPOS.Persistence.Services.PaymentServices
{
    public class OtpService : IOtpService
    {
        public string GenerateOtp()
        {
            var rnd = new Random();
            return rnd.Next(100000, 999999).ToString();
        }
    }
}
