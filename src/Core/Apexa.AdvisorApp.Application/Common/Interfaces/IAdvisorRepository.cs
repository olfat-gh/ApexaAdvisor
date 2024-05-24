using Apexa.AdvisorApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apexa.AdvisorApp.Application.Common.Interfaces
{
    public interface IAdvisorRepository : IAsyncRepository<Advisor>
    {

        Task<Advisor> GetByIdAsync(Guid id);
        Task<(int, List<Advisor>)> GetAdvisorsAsync(int pageIndex,int pageSize);
    }
}
