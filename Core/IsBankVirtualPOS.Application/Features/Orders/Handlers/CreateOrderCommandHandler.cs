using IsBankVirtualPOS.Application.Features.Orders.Commands;
using IsBankVirtualPOS.Application.Interfaces.CommonInterfaces;
using IsBankVirtualPOS.Application.Interfaces.OrderInterfaces;
using IsBankVirtualPOS.Domain.Entities;
using IsBankVirtualPOS.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsBankVirtualPOS.Application.Features.Orders.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IGenericRepository<Order> _orderRepository;

        public CreateOrderCommandHandler(IGenericRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Order
            {
                Id = Guid.NewGuid(),
                AppUserId = request.AppUserId,
                Amount = request.Amount,
                Currency = request.Currency,
                Status = OrderStatus.Pending,
                CreatedDate = DateTime.UtcNow,
                OrderNumber = GenerateOrderNumber()
            };

            await _orderRepository.AddAsync(order);
            await _orderRepository.SaveChangesAsync();

            return order.Id;
        }

        private string GenerateOrderNumber()
        {
            return $"ORD-{DateTime.UtcNow.Ticks}";
        }
    }
}
