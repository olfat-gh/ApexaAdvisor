using Apexa.AdvisorApp.Application.Advisors.Commands.CreateAdvisor;
using Apexa.AdvisorApp.Contracts.V1.Advisor;
using AutoMapper;

namespace Apexa.AdvisorApp.WebApi.Mappings
{
    public class ApiMapperProfile : Profile
    {
        public ApiMapperProfile()
        {
            CreateMap<CreateAdvisorApiRequest, CreateAdvisorCommand>();

        }
    }
}
