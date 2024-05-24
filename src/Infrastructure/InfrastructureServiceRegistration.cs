using Apexa.AdvisorApp.Application.Common.Interfaces;
using Apexa.AdvisorApp.Infrastructure.Advisors.Persistence;
using Apexa.AdvisorApp.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apexa.AdvisorApp.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {

        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<AdvisorAppDbContext>(options =>
               options.UseInMemoryDatabase("AdvisorDB"));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IAdvisorRepository, AdvisorRepository>();
            return services;
        }

    }
}
