using IsBankVirtualPOS.Application.Interfaces.CommonInterfaces;
using IsBankVirtualPOS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsBankVirtualPOS.Application.Interfaces.PaymentAttemptInterfaces
{
    public interface IPaymentAttemptRepository : IGenericRepository<PaymentAttempt>
    {
        Task<int> GetPaymentAttemptCountByPaymentIdAsync(Guid id);
        Task<bool> IsAttemptExistsByBankRefAsync(string bankRef);
    }
}
