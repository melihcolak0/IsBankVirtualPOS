using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsBankVirtualPOS.Domain.Enums
{
    public enum PaymentStatus
    {
        Pending = 0,
        Success = 1,
        Failed = 2,
        Cancelled = 3,
        Refunded = 4,
        PreAuthorized = 5
    }
}
