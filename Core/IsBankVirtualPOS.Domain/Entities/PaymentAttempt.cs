using IsBankVirtualPOS.Domain.Common;
using IsBankVirtualPOS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsBankVirtualPOS.Domain.Entities
{
    public class PaymentAttempt : BaseEntity
    {
        public Guid PaymentId { get; set; }
        public Payment Payment { get; set; }

        public int AttemptNumber { get; set; }

        public PaymentAttemptStatus Status { get; set; }

        public string ConversationId { get; set; }

        public decimal Amount { get; set; }

        public string CardLast4 { get; set; }
        public string CardHolder { get; set; }

        public bool IsSuccess { get; set; }
        public string? AuthCode { get; set; }
        public string? BankReferenceNumber { get; set; }
        public string? ResponseCode { get; set; }
        public string? ResponseMessage { get; set; }

        public string? RawRequest { get; set; }
        public string? RawResponse { get; set; }

        public DateTime AttemptDate { get; set; }

        public ICollection<PaymentTransaction> Transactions { get; set; }
       
    }
}
