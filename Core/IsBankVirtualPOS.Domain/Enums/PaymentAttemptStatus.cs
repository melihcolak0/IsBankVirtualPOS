using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsBankVirtualPOS.Domain.Enums
{
    public enum PaymentAttemptStatus
    {
        Started = 0,      
        Waiting3DS = 1,     
        Completed = 2,      
        Failed = 3        
    }
}
