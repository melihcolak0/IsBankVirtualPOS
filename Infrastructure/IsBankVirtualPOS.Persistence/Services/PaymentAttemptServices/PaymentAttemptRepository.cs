using IsBankVirtualPOS.Application.Interfaces.PaymentAttemptInterfaces;
using IsBankVirtualPOS.Domain.Entities;
using IsBankVirtualPOS.Persistence.DbContexts;
using IsBankVirtualPOS.Persistence.Services.CommonServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsBankVirtualPOS.Persistence.Services.PaymentAttemptServices
{
    public class PaymentAttemptRepository : GenericRepository<PaymentAttempt>, IPaymentAttemptRepository
    {
        private readonly AppDbContext _context;

        public PaymentAttemptRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> GetPaymentAttemptCountByPaymentIdAsync(Guid id)
        {
            return await _context.PaymentAttempts.CountAsync(x => x.PaymentId == id);
        }

        public async Task<bool> IsAttemptExistsByBankRefAsync(string bankRef)
        {
            return await _context.PaymentAttempts.AnyAsync(x => x.BankReferenceNumber == bankRef);
        }
    }
}
