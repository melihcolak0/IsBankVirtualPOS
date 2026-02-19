using IsBankVirtualPOS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsBankVirtualPOS.Domain.Entities
{
    public class Refund : BaseEntity
    {
        public Guid PaymentId { get; set; }
        public Payment Payment { get; set; }

        public decimal Amount { get; set; }

        public string Reason { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
