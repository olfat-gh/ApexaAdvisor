using Apexa.AdvisorApp.Application.Common.Interfaces;
using Apexa.AdvisorApp.Domain.Entities;
using Apexa.AdvisorApp.Infrastructure.Common.Persistence;
using Apexa.Lib.Cache.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apexa.AdvisorApp.Infrastructure.Advisors.Persistence
{

    public class CachedAdvisorRepository : IAdvisorRepository
    {
        private readonly IAdvisorRepository _advisorRepository;
        private readonly ILogger<AdvisorRepository> _logger;
        private readonly ICacheService<Advisor> _cacheService;
        public CachedAdvisorRepository(ILogger<AdvisorRepository> logger,
            IAdvisorRepository advisorRepository,
            ICacheService<Advisor> cacheService
            ) 
        {
            _logger = logger;
            _advisorRepository = advisorRepository;
            _cacheService = cacheService;
        }
        public async Task<Advisor> GetByIdAsync(Guid id)
        {
            //first check the cache
            if (!_cacheService.TryGetValue(id.ToString(), out Advisor advisor))
            {
                advisor = await _advisorRepository.GetByIdAsync(id);
                if (advisor != null)
                    _cacheService.PutValue(id.ToString(), advisor);
            }
            return advisor;
        }

        public async Task<(int, List<Advisor>)> GetAdvisorsAsync(int pageIndex, int pageSize)
        {
            
            return await _advisorRepository.GetAdvisorsAsync(pageIndex, pageSize);
        }


        public async Task<Advisor> AddAsync(Advisor entity)
        {
            var result = await _advisorRepository.AddAsync(entity);
            _cacheService.PutValue(result.Id.ToString(), result);

            return result;
        }

        public async Task UpdateAsync(Advisor entity)
        { 
             await _advisorRepository.UpdateAsync(entity);
            _cacheService.PutValue(entity.Id.ToString(), entity);
        }

        public async Task DeleteAsync(Advisor entity)
        {
            await _advisorRepository.DeleteAsync(entity);
            _cacheService.RemoveValue(entity.Id.ToString());  

        }
    }
}
