using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsBankVirtualPOS.Application.Features.Payments.Commands
{
    public class VerifyOtpCommand : IRequest<bool>
    {
        public Guid PaymentId { get; set; }
        public string Otp { get; set; }
    }
}
