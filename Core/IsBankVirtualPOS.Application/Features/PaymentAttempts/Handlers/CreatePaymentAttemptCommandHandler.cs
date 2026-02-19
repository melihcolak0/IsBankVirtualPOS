using IsBankVirtualPOS.Application.DTOs.PaymentAttempt;
using IsBankVirtualPOS.Application.Features.PaymentAttempts.Commands;
using IsBankVirtualPOS.Application.Interfaces.CommonInterfaces;
using IsBankVirtualPOS.Application.Interfaces.PaymentAttemptInterfaces;
using IsBankVirtualPOS.Application.Interfaces.PaymentInterfaces;
using IsBankVirtualPOS.Domain.Entities;
using IsBankVirtualPOS.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsBankVirtualPOS.Application.Features.PaymentAttempts.Handlers
{
    public class CreatePaymentAttemptCommandHandler : IRequestHandler<CreatePaymentAttemptCommand, Guid>
    {
        private readonly IPaymentAttemptRepository _paymentAttemptRepository;
        private readonly IPaymentProvider _paymentProvider;
        private readonly IGenericRepository<Payment> _paymentRepository;
        private readonly IGenericRepository<PaymentTransaction> _transactionRepository;

        public CreatePaymentAttemptCommandHandler(IPaymentAttemptRepository paymentAttemptRepository, IPaymentProvider paymentProvider, IGenericRepository<Payment> paymentRepository, IGenericRepository<PaymentTransaction> transactionRepository)
        {
            _paymentAttemptRepository = paymentAttemptRepository;
            _paymentProvider = paymentProvider;
            _paymentRepository = paymentRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task<Guid> Handle(CreatePaymentAttemptCommand request, CancellationToken cancellationToken)
        {
            var attemptCount = await _paymentAttemptRepository.GetPaymentAttemptCountByPaymentIdAsync(request.PaymentId);

            var attempt = new PaymentAttempt
            {
                Id = Guid.NewGuid(),
                PaymentId = request.PaymentId,
                AttemptNumber = attemptCount + 1,
                ConversationId = Guid.NewGuid().ToString(),
                Amount = request.Amount,
                AttemptDate = DateTime.UtcNow,
                IsSuccess = false
            };

            await _paymentAttemptRepository.AddAsync(attempt);

            var result = await _paymentProvider.ProcessAsync(new PaymentRequest
            {
                PaymentId = request.PaymentId,
                Amount = request.Amount,
                ConversationId = attempt.ConversationId
            });

            var transaction = new PaymentTransaction
            {
                Id = Guid.NewGuid(),

                PaymentAttempt = attempt,

                RequestPayload = result.RawRequest,
                ResponsePayload = result.RawResponse,
                IsSuccess = result.IsSuccess,
                BankReferenceNumber = result.BankReferenceNumber,
                CreatedDate = DateTime.UtcNow
            };

            await _transactionRepository.AddAsync(transaction);

            attempt.IsSuccess = result.IsSuccess;
            attempt.AuthCode = result.AuthCode;
            attempt.ResponseCode = result.BankResponseCode;
            attempt.BankReferenceNumber = result.BankReferenceNumber;
            attempt.RawRequest = result.RawRequest;
            attempt.RawResponse = result.RawResponse;

            var payment = await _paymentRepository.GetByIdAsync(request.PaymentId);

            payment.Status = result.IsSuccess
                ? PaymentStatus.Success
                : PaymentStatus.Failed;

            _paymentRepository.Update(payment);

            await _paymentAttemptRepository.SaveChangesAsync();

            return attempt.Id;            
        }
    }
}
