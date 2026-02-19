using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsBankVirtualPOS.Application.Features.Payments.Results
{
    public class GetPaymentSummaryQueryResult
    {
        public Guid PaymentId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public bool CanRefund { get; set; }
        public int AttemptCount { get; set; }
    }
}
