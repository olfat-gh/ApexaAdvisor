using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apexa.Lib.Cache.Configuation;
using Apexa.Lib.Cache.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Apexa.Lib.Cache.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCachingSystem(this IServiceCollection services, IConfiguration configuration,
      string sectionName = "")
        {

            var section =
                  configuration.GetSection(string.IsNullOrEmpty(sectionName) ? CacheSettings.SectionName : sectionName);

            services.AddOptions<CacheSettings>().Configure(section.Bind);
            services.AddSingleton(typeof(ICacheService<>), typeof(CacheService<>));

            return services;
        }
    }
}
