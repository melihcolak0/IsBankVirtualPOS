using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsBankVirtualPOS.Application.Features.Orders.Commands
{
    public class CreateOrderCommand : IRequest<Guid>
    {
        public Guid AppUserId { get; set; }

        public decimal Amount { get; set; }

        public string Currency { get; set; } = "TRY";
    }
}
