using IsBankVirtualPOS.Domain.Common;
using IsBankVirtualPOS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsBankVirtualPOS.Domain.Entities
{
    public class Order : BaseEntity
    {
        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public string OrderNumber { get; set; }

        public decimal Amount { get; set; }
        public string Currency { get; set; } = "TRY";

        public OrderStatus Status { get; set; }

        public DateTime CreatedDate { get; set; }

        public ICollection<Payment> Payments { get; set; }
    }
}
