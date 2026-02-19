using IsBankVirtualPOS.Application.Features.Payments.Commands;
using IsBankVirtualPOS.Application.Interfaces.CommonInterfaces;
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
    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, Guid>
    {
        private readonly IGenericRepository<Payment> _paymentRepository;

        public CreatePaymentCommandHandler(IGenericRepository<Payment> paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<Guid> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                OrderId = request.OrderId,
                Amount = request.Amount,
                Provider = request.Provider,
                Status = PaymentStatus.Pending,
                ConversationId = Guid.NewGuid().ToString(),
                CreatedDate = DateTime.UtcNow
            };

            await _paymentRepository.AddAsync(payment);
            await _paymentRepository.SaveChangesAsync();

            return payment.Id;
        }
    }
}
