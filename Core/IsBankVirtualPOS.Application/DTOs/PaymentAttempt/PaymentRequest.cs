using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsBankVirtualPOS.Application.DTOs.PaymentAttempt
{
    public class PaymentRequest
    {
        public Guid PaymentId { get; set; }
        public decimal Amount { get; set; }
        public string ConversationId { get; set; }
    }
}
