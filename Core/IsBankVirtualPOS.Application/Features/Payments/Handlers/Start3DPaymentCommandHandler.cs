using IsBankVirtualPOS.Application.DTOs.Payment;
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
    public class Start3DPaymentCommandHandler : IRequestHandler<Start3DPaymentCommand, ThreeDPaymentFormDTO>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IPaymentAttemptRepository _paymentAttemptRepository;
        private readonly IOtpService _otpService;
        private readonly ISmtpMailService _mailService;

        public Start3DPaymentCommandHandler(IPaymentRepository paymentRepository, IPaymentAttemptRepository paymentAttemptRepository, ISmtpMailService mailService, IOtpService otpService)
        {
            _paymentRepository = paymentRepository;
            _paymentAttemptRepository = paymentAttemptRepository;
            _mailService = mailService;
            _otpService = otpService;
        }

        public async Task<ThreeDPaymentFormDTO> Handle(Start3DPaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository.GetByIdAsync(request.PaymentId);

            if (payment == null)
                throw new Exception("Payment not found");

            var attempt = new PaymentAttempt
            {
                Id = Guid.NewGuid(),
                PaymentId = payment.Id,
                AttemptDate = DateTime.UtcNow,
                Status = PaymentAttemptStatus.Waiting3DS,
                Amount = payment.Amount,
                AttemptNumber = (payment.Attempts?.Count ?? 0) + 1,
                CardLast4 = request.CardNumber[^4..],
                CardHolder = request.CardHolder,
                ConversationId = Guid.NewGuid().ToString()
            };

            await _paymentAttemptRepository.AddAsync(attempt);
            payment.Status = PaymentStatus.Pending;

            // OTP üret
            var otp = _otpService.GenerateOtp();
            payment.OtpCode = otp;
            payment.OtpExpireAt = DateTime.UtcNow.AddMinutes(5);
            payment.OtpVerified = false;

            var userEmail = _paymentRepository.GetUserEmailByPaymentIdAsync(payment.Id).Result;

            _mailService.SendOtpMail(userEmail, otp);

            await _paymentRepository.SaveChangesAsync();

            // artýk sadece data dönüyoruz
            return new ThreeDPaymentFormDTO
            {
                CallbackUrl = "https://localhost:7290/api/payments/3d-callback",
                PaymentId = request.PaymentId,
                MdStatus = "1",
                ResponseCode = "00",
                AuthCode = "123456",
                BankReferenceNumber = Guid.NewGuid().ToString()
            };
        }
    }    
}
