using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsBankVirtualPOS.Application.Interfaces.PaymentInterfaces
{
    public interface IOtpService
    {
        public string GenerateOtp();
    }
}
