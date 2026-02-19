using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsBankVirtualPOS.Application.DTOs.Payment
{
    public class ThreeDPaymentFormDTO
    {
        public string CallbackUrl { get; set; } = default!;
        public Guid PaymentId { get; set; }
        public string MdStatus { get; set; } = default!;
        public string ResponseCode { get; set; } = default!;
        public string AuthCode { get; set; } = default!;
        public string BankReferenceNumber { get; set; } = default!;
    }
}
