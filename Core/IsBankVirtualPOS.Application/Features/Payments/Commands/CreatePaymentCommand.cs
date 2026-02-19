using IsBankVirtualPOS.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsBankVirtualPOS.Application.Features.Payments.Commands
{
    public class CreatePaymentCommand : IRequest<Guid>
    {
        public Guid OrderId { get; set; }

        public decimal Amount { get; set; }

        public PaymentProvider Provider { get; set; }
    }
}
