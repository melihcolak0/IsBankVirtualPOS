using IsBankVirtualPOS.Application.Features.Payments.Commands;
using IsBankVirtualPOS.Application.Interfaces.PaymentAttemptInterfaces;
using IsBankVirtualPOS.Application.Interfaces.PaymentInterfaces;
using IsBankVirtualPOS.Domain.Entities;
using IsBankVirtualPOS.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsBankVirtualPOS.Application.Features.Payments.Handlers
{
    public class StartPaymentCommandHandler : IRequestHandler<StartPaymentCommand, string?>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IPaymentAttemptRepository _paymentAttemptRepository;

        public StartPaymentCommandHandler(IPaymentRepository paymentRepository, IPaymentAttemptRepository paymentAttemptRepository)
        {
            _paymentRepository = paymentRepository;
            _paymentAttemptRepository = paymentAttemptRepository;
        }

        public async Task<string?> Handle(StartPaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository.GetByIdAsync(request.PaymentId);

            if (payment == null)
                throw new Exception("Payment not found");

            var attempt = new PaymentAttempt
            {
                Id = Guid.NewGuid(),
                PaymentId = payment.Id,
                AttemptDate = DateTime.UtcNow,
                Status = PaymentAttemptStatus.Started,
                Amount = payment.Amount,
                AttemptNumber = (payment.Attempts?.Count ?? 0) + 1,
                CardLast4 = request.CardNumber[^4..],
                CardHolder = request.CardHolder,
                ConversationId = Guid.NewGuid().ToString()
            };

            await _paymentAttemptRepository.AddAsync(attempt);

            // NON-3D ödeme simülasyonu
            attempt.Status = PaymentAttemptStatus.Completed;
            attempt.IsSuccess = true;
            attempt.AuthCode = "NON3D123";
            attempt.ResponseCode = "00";

            payment.Status = PaymentStatus.Success;

            await _paymentRepository.SaveChangesAsync();

            return null;
        }        
    }
}
