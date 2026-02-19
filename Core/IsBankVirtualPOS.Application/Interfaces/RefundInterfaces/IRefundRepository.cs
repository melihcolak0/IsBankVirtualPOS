using IsBankVirtualPOS.Application.Interfaces.CommonInterfaces;
using IsBankVirtualPOS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IsBankVirtualPOS.Application.Interfaces.RefundInterfaces
{
    public interface IRefundRepository : IGenericRepository<Refund>
    {
    }
}
