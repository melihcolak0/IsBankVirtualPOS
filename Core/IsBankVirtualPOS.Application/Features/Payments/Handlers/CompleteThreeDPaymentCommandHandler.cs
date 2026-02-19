using IsBankVirtualPOS.Application.Features.Payments.Commands;
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
using System.Text.Json;
using System.Threading.Tasks;

namespace IsBankVirtualPOS.Application.Features.Payments.Handlers
{
    public class CompleteThreeDPaymentCommandHandler : IRequestHandler<CompleteThreeDPaymentCommand>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IPaymentAttemptRepository _paymentAttemptRepository;
        private readonly IGenericRepository<PaymentTransaction> _paymentTransactionRepository;

        public CompleteThreeDPaymentCommandHandler(IPaymentRepository paymentRepository, IPaymentAttemptRepository paymentAttemptRepository, IGenericRepository<PaymentTransaction> paymentTransactionRepository)
        {
            _paymentRepository = paymentRepository;
            _paymentAttemptRepository = paymentAttemptRepository;
            _paymentTransactionRepository = paymentTransactionRepository;
        }

        public async Task Handle(CompleteThreeDPaymentCommand request, CancellationToken cancellationToken)
        {
            var payment =
        await _paymentRepository.GetPaymentByPaymentIdWithAttemptsAsync(request.PaymentId);

            if (payment == null)
                throw new Exception("Payment not found");

            // duplicate callback korumasý
            if (await _paymentAttemptRepository
                .IsAttemptExistsByBankRefAsync(request.BankReferenceNumber))
                return;

            // BEKLEYEN 3D ATTEMPT BUL
            var attempt = payment.Attempts
                .OrderByDescending(a => a.AttemptDate)
                .FirstOrDefault(a => a.Status == PaymentAttemptStatus.Waiting3DS);

            if (attempt == null)
                throw new Exception("Waiting 3D attempt not found");

            bool success =
                request.MdStatus == "1" &&
                request.ResponseCode == "00";

            // INSERT YOK — UPDATE VAR
            attempt.IsSuccess = success;
            attempt.Status = success
                ? PaymentAttemptStatus.Completed
                : PaymentAttemptStatus.Failed;

            attempt.AuthCode = request.AuthCode;
            attempt.ResponseCode = request.ResponseCode;
            attempt.BankReferenceNumber = request.BankReferenceNumber;

            // TRANSACTION LOG
            var transaction = new PaymentTransaction
            {
                Id = Guid.NewGuid(),
                PaymentAttemptId = attempt.Id,
                IsSuccess = success,
                BankReferenceNumber = request.BankReferenceNumber,
                RequestPayload = "3D CALLBACK",
                ResponsePayload = JsonSerializer.Serialize(request)
            };

            await _paymentTransactionRepository.AddAsync(transaction);

            payment.Status = success
                ? PaymentStatus.Success
                : PaymentStatus.Failed;

            await _paymentRepository.SaveChangesAsync();            
        }
    }
}
