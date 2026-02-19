using IsBankVirtualPOS.Application.Features.Refunds.Commands;
using IsBankVirtualPOS.Application.Interfaces.CommonInterfaces;
using IsBankVirtualPOS.Application.Interfaces.PaymentInterfaces;
using IsBankVirtualPOS.Application.Interfaces.RefundInterfaces;
using IsBankVirtualPOS.Domain.Entities;
using IsBankVirtualPOS.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsBankVirtualPOS.Application.Features.Refunds.Handlers
{
    public class CreateRefundCommandHandler : IRequestHandler<CreateRefundCommand, Guid>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IGenericRepository<Refund> _refundRepository;

        public CreateRefundCommandHandler(IPaymentRepository paymentRepository, IGenericRepository<Refund> refundRepository)
        {
            _paymentRepository = paymentRepository;
            _refundRepository = refundRepository;
        }

        public async Task<Guid> Handle(CreateRefundCommand request, CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository
                .GetByIdAsync(request.PaymentId);

            if (payment == null)
                throw new Exception("Payment not found");

            if (!payment.CanRefund())
                throw new Exception("Payment cannot be refunded");

            if (request.Amount > payment.Amount)
                throw new Exception("Refund amount exceeds payment");

            var refund = new Refund
            {
                Id = Guid.NewGuid(),
                PaymentId = payment.Id,
                Amount = request.Amount,
                Reason = request.Reason,
                CreatedDate = DateTime.UtcNow
            };

            await _refundRepository.AddAsync(refund);

            payment.Status = PaymentStatus.Refunded;

            _paymentRepository.Update(payment);
            await _paymentRepository.SaveChangesAsync();

            return refund.Id;
        }
    }
}
