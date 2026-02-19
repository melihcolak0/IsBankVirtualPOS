using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsBankVirtualPOS.Application.Features.Payments.Commands
{
    public class StartPaymentCommand : IRequest<string?>
    {
        public Guid PaymentId { get; set; }

        public string CardHolder { get; set; }
        public string CardNumber { get; set; }
        public string ExpireMonth { get; set; }
        public string ExpireYear { get; set; }
        public string CVV { get; set; }

        public bool Use3D { get; set; }
    }
}
