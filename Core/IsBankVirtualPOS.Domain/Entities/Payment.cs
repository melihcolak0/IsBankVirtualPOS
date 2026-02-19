using IsBankVirtualPOS.Domain.Common;
using IsBankVirtualPOS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsBankVirtualPOS.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        public decimal Amount { get; set; }

        public PaymentStatus Status { get; set; }

        public PaymentProvider Provider { get; set; }

        public string ConversationId { get; set; }

        public DateTime CreatedDate { get; set; }

        public ICollection<PaymentAttempt> Attempts { get; set; }
        public ICollection<Refund> Refunds { get; set; }

        public string? OtpCode { get; set; }
        public DateTime? OtpExpireAt { get; set; }
        public bool OtpVerified { get; set; }

        public bool CanRefund()
        {
            return Status == PaymentStatus.Success;
        }

        public PaymentAttempt? GetWaiting3DAttempt()
        {
            return Attempts
                .OrderByDescending(a => a.AttemptDate)
                .FirstOrDefault(a => a.Status == PaymentAttemptStatus.Waiting3DS);
        }

        public PaymentAttempt CreateAttempt(string cardHolder, string cardLast4, decimal amount)
        {
            var attempt = new PaymentAttempt
            {
                Id = Guid.NewGuid(),
                PaymentId = this.Id,
                AttemptDate = DateTime.UtcNow,
                AttemptNumber = Attempts.Count + 1,
                CardHolder = cardHolder,
                CardLast4 = cardLast4,
                Amount = amount,
                Status = PaymentAttemptStatus.Started,
                ConversationId = Guid.NewGuid().ToString()
            };

            Attempts.Add(attempt);
            return attempt;
        }
    }
}
