using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Apexa.AdvisorApp.WebApi.Utility
{
    /// <summary>
    /// applying actions attributes to swagger 
    /// </summary>
    public class SwaggerDefaultValues : IOperationFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var apiDescription = context.ApiDescription;
            operation.Deprecated |= apiDescription.IsDeprecated();

            if (operation.Parameters == null)
                return;

            // REF: https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/412
            // REF: https://github.com/domaindrivendev/Swashbuckle.AspNetCore/pull/413
            foreach (var parameter in operation.Parameters)
            {
                var description = apiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);

                parameter.Description ??= description.ModelMetadata?.Description;

                if (parameter.Schema.Default == null && description.DefaultValue != null)
                {
                    parameter.Schema.Default = new OpenApiString(description.DefaultValue.ToString());
                }

                parameter.Required |= description.IsRequired;
            }

            var scheme = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearer" }
            };
            operation.Security.Add(new OpenApiSecurityRequirement { [scheme] = new List<string>() });
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class ConfigureSwagger : IConfigureOptions<SwaggerGenOptions>
    {

        private readonly IApiVersionDescriptionProvider _provider;
        private readonly ApexaHostOption _hostOptions;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        public ConfigureSwagger(IApiVersionDescriptionProvider provider, IOptions<ApexaHostOption> hostOptions)
        {
            _provider = provider;
            _hostOptions = hostOptions.Value;
        }

        void IConfigureOptions<SwaggerGenOptions>.Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description, _hostOptions));
            }
        }

        static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description, ApexaHostOption option)
        {
            var info = new OpenApiInfo()
            {
                Title = option.SystemName,
                Version = option.Version,
                Description = option.SystemDescription,
            };

            if (description.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }

            return info;
        }
    }
}
