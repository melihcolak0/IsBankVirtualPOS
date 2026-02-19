using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsBankVirtualPOS.Application.DTOs.Payment
{
    public class ThreeDCallbackRequest
    {
        public Guid PaymentId { get; set; }

        public string MdStatus { get; set; } // 1 = success
        public string ResponseCode { get; set; } // 00 success
        public string AuthCode { get; set; }
        public string BankReferenceNumber { get; set; }
    }
}
