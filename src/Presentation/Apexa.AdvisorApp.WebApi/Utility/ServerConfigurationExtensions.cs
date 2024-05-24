using Apexa.AdvisorApp.WebApi.Mappings;
using Apexa.AdvisorApp.WebApi.Utility;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using AutoMapper;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Apexa.AdvisorApp.WebApi.Extensions
{
    public static class AppHelper
    {
        internal static void ConfigApplication(WebApplicationBuilder builder, string[] args)
        {
            builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("host.json", false, true)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", false, true)
                .AddCommandLine(args);
        }
        internal static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwagger>();
            services.AddSwaggerGen(options =>
            {
                options.OperationFilter<SwaggerDefaultValues>();
            });

            return services;
        }
        internal static IServiceCollection AddApexaApiVersioning(this IServiceCollection services)
        {

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new QueryStringApiVersionReader("api-version"),
                    new HeaderApiVersionReader("api-version"));
            }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
            });

            return services;
        }
        internal static WebApplication EnableSwagger(this WebApplication app)
        {
            var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                // build a swagger endpoint for each discovered API version
                foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
            });

            return app;
        }

        internal static IServiceCollection ConfigSystemInfo(this IServiceCollection services,
    IConfiguration configuration)
        {
            var version = configuration.GetSection("ApplicationVersion")?.Value;
            var applicationName = configuration.GetSection("SystemName")?.Value;
            var description = configuration.GetSection("SystemDescription")?.Value;
            if (string.IsNullOrEmpty(version))
            {
                throw new Exception("cannot find ApplicationVersion in host.json file");
            }
            if (string.IsNullOrEmpty(applicationName))
            {
                throw new Exception("cannot find SystemName in host.json file");
            }
            if (string.IsNullOrEmpty(description))
            {
                throw new Exception("cannot find SystemDescription in host.json file");
            }
            services.Configure<ApexaHostOption>(config =>
            {

                config.Version = version;
                config.SystemName = applicationName;
                config.SystemDescription = description;
            });


            return services;
        }

        internal static IServiceCollection RegisterAutoMapperServices(this IServiceCollection services, Type[] profilesToAdd, Assembly assembly)
        {
           
            services.AddAutoMapper(AutoMappingConfig.GetConfig(profilesToAdd), assembly);
            return services;
        }


    }
}
