using IsBankVirtualPOS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IsBankVirtualPOS.Application.Interfaces.CommonInterfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task AddAsync(T entity);

        Task<T?> GetByIdAsync(Guid id);

        Task<List<T>> GetAllAsync();

        Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate);

        void Update(T entity);

        void Remove(T entity);

        Task SaveChangesAsync();
    }
}
