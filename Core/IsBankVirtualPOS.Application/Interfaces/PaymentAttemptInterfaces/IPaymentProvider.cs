using IsBankVirtualPOS.Application.DTOs.PaymentAttempt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsBankVirtualPOS.Application.Interfaces.PaymentAttemptInterfaces
{
    public interface IPaymentProvider
    {
        Task<PaymentResult> ProcessAsync(PaymentRequest request);
    }
}
