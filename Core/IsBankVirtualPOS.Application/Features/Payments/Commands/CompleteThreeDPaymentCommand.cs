using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsBankVirtualPOS.Application.Features.Payments.Commands
{
    public class CompleteThreeDPaymentCommand : IRequest
    {
        public Guid PaymentId { get; set; }
        public string MdStatus { get; set; }
        public string ResponseCode { get; set; }
        public string AuthCode { get; set; }
        public string BankReferenceNumber { get; set; }
    }
}
