using IsBankVirtualPOS.Application.Features.Payments.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsBankVirtualPOS.Application.Features.Payments.Queries
{
    public class GetPaymentSummaryQuery : IRequest<GetPaymentSummaryQueryResult>
    {
        public Guid PaymentId { get; set; }

        public GetPaymentSummaryQuery(Guid paymentId)
        {
            PaymentId = paymentId;
        }
    }
}
