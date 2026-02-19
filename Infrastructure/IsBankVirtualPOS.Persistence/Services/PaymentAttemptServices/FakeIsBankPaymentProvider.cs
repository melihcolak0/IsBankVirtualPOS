using IsBankVirtualPOS.Application.DTOs.PaymentAttempt;
using IsBankVirtualPOS.Application.Interfaces.PaymentAttemptInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsBankVirtualPOS.Persistence.Services.PaymentAttemptServices
{
    public class FakeIsBankPaymentProvider : IPaymentProvider
    {
        public async Task<PaymentResult> ProcessAsync(PaymentRequest request)
        {
            await Task.Delay(500);

            var fakeRequest = System.Text.Json.JsonSerializer.Serialize(request);

            var fakeResponse = """
            {
                "responseCode": "00",
                "authCode": "999999",
                "bankReferenceNumber": "ISBANK123456"
            }
            """;

            return new PaymentResult
            {
                IsSuccess = true,
                AuthCode = "999999",
                BankResponseCode = "00",
                BankReferenceNumber = "ISBANK123456",
                RawRequest = fakeRequest,
                RawResponse = fakeResponse
            };
        }
    }
}
