using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsBankVirtualPOS.Application.DTOs.PaymentAttempt
{
    public class PaymentResult
    {
        public bool IsSuccess { get; set; }
        public string? AuthCode { get; set; }
        public string? ErrorMessage { get; set; }
        public string? BankResponseCode { get; set; }

        public string? BankReferenceNumber { get; set; }
        public string? RawRequest { get; set; }
        public string? RawResponse { get; set; }
    }
}
