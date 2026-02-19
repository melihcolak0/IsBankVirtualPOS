using IsBankVirtualPOS.Application.Features.Payments.Commands;
using IsBankVirtualPOS.Application.Interfaces.PaymentInterfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsBankVirtualPOS.Application.Features.Payments.Handlers
{
    public class VerifyOtpCommandHandler : IRequestHandler<VerifyOtpCommand, bool>
    {
        private readonly IPaymentRepository _paymentRepository;

        public VerifyOtpCommandHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<bool> Handle(VerifyOtpCommand request, CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository
                .GetByIdAsync(request.PaymentId);

            if (payment == null)
                throw new Exception("Payment not found");

            if (payment.OtpExpireAt < DateTime.UtcNow)
                return false;

            if (payment.OtpCode != request.Otp)
                return false;

            payment.OtpVerified = true;

            await _paymentRepository.SaveChangesAsync();

            return true;
        }
    }
}
