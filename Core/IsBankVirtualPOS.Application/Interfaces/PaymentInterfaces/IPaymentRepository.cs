using IsBankVirtualPOS.Application.Interfaces.CommonInterfaces;
using IsBankVirtualPOS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsBankVirtualPOS.Application.Interfaces.PaymentInterfaces
{
    public interface IPaymentRepository : IGenericRepository<Payment>
    {
        Task<Payment> GetPaymentByPaymentIdWithAttemptsAsync(Guid id);
        Task<string> GetUserEmailByPaymentIdAsync(Guid id);
    }
}
