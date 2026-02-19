using IsBankVirtualPOS.Application.Features.Payments.Queries;
using IsBankVirtualPOS.Application.Features.Payments.Results;
using IsBankVirtualPOS.Application.Interfaces.PaymentInterfaces;
using IsBankVirtualPOS.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsBankVirtualPOS.Application.Features.Payments.Handlers
{
    public class GetPaymentSummaryQueryHandler : IRequestHandler<GetPaymentSummaryQuery, GetPaymentSummaryQueryResult>
    {
        private readonly IPaymentRepository _paymentRepository;

        public GetPaymentSummaryQueryHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<GetPaymentSummaryQueryResult> Handle(GetPaymentSummaryQuery request, CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository.GetPaymentByPaymentIdWithAttemptsAsync(request.PaymentId);

            if (payment == null)
                throw new Exception("Payment not found");

            return new GetPaymentSummaryQueryResult
            {
                PaymentId = payment.Id,
                Amount = payment.Amount,
                Status = payment.Status.ToString(),
                AttemptCount = payment.Attempts.Count,
                CanRefund = payment.Status == PaymentStatus.Success
            };
        }
    }
}
