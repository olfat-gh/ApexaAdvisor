using Apexa.AdvisorApp.Application.Mappings;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Apexa.AdvisorApp.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            });

            return services;
        }
        public static IServiceCollection RegisterAutoMapperServices(this IServiceCollection services, Type[] profilesToAdd, Assembly assembly)
        {

            services.AddAutoMapper(AutoMappingConfig.GetConfig(profilesToAdd), assembly);
            return services;
        }
    }
}
