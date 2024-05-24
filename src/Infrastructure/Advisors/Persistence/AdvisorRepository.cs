using Apexa.AdvisorApp.Application.Common.Interfaces;
using Apexa.AdvisorApp.Domain.Entities;
using Apexa.AdvisorApp.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
