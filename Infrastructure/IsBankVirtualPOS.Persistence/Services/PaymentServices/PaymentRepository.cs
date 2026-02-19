using Azure.Core;
using IsBankVirtualPOS.Application.Interfaces.PaymentAttemptInterfaces;
using IsBankVirtualPOS.Application.Interfaces.PaymentInterfaces;
using IsBankVirtualPOS.Domain.Entities;
using IsBankVirtualPOS.Persistence.DbContexts;
using IsBankVirtualPOS.Persistence.Services.CommonServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IsBankVirtualPOS.Persistence.Services.PaymentServices
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        private readonly AppDbContext _context;

        public PaymentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Payment> GetPaymentByPaymentIdWithAttemptsAsync(Guid id)
        {
            return await _context.Payments.Include(p => p.Attempts).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<string> GetUserEmailByPaymentIdAsync(Guid id)
        {
            return await _context.Payments
                                .Where(p => p.Id == id)
                                .Select(p => p.Order.AppUser.Email)
                                .FirstOrDefaultAsync();
        }
    }
}
