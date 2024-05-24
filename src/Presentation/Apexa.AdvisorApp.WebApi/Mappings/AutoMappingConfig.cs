using AutoMapper;
using System;

namespace Apexa.AdvisorApp.WebApi.Mappings
{
    public class AutoMappingConfig
    {
        public static Action<IMapperConfigurationExpression> GetConfig(Type[] profiles)
        {
            return cfg => cfg.AddMaps(profiles);
        }
    }
}
