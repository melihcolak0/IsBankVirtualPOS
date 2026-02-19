using IsBankVirtualPOS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsBankVirtualPOS.Domain.Entities
{
    public class PaymentTransaction : BaseEntity
    {
        public Guid PaymentAttemptId { get; set; }
        public PaymentAttempt PaymentAttempt { get; set; }

        public string? RequestPayload { get; set; }
        public string? ResponsePayload { get; set; }

        public bool IsSuccess { get; set; }

        public string? BankReferenceNumber { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
