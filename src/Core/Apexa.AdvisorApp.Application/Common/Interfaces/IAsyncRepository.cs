using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apexa.AdvisorApp.Application.Common.Interfaces
{
    public interface IAsyncRepository<T> where T : class
    {
        Task<T> GetByIdAsync(Guid Id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(Guid Id);

    }
}
