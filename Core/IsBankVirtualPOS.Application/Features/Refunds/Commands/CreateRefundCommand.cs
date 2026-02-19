using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsBankVirtualPOS.Application.Features.Refunds.Commands
{
    public class CreateRefundCommand : IRequest<Guid>
    {
        public Guid PaymentId { get; set; }
        public decimal Amount { get; set; }
        public string Reason { get; set; }
    }
}
