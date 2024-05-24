using Apexa.AdvisorApp.Application.Common.Interfaces;
using Apexa.AdvisorApp.Domain.Entities;
using Apexa.AdvisorApp.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Apexa.AdvisorApp.Infrastructure.Advisors.Persistence
{
    public class AdvisorRepository : BaseRepository<Advisor>, IAdvisorRepository
    {

        public AdvisorRepository(AdvisorAppDbContext dbContext) : base(dbContext)
        {
            
        }

        public async Task<Advisor?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Advisors.FirstOrDefaultAsync(item => item.Id == id);
        }

        public async Task<(int, List<Advisor>)> GetAdvisorsAsync(int pageIndex, int pageSize)
        {
            var query = _dbContext.Advisors.AsQueryable();
            var totalPages = (int)Math.Ceiling((double)query.Count() / pageSize);
            var advisors = await query
                           .Skip((pageIndex - 1) * pageSize)
                           .Take(pageSize).ToListAsync();
            return (totalPages,advisors);
        }
    }
}
