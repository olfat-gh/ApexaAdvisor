using Apexa.AdvisorApp.Application.Advisors.Commands.CreateAdvisor;
using Apexa.AdvisorApp.Application.Advisors.Queries.GetAdvisorList;
using Apexa.AdvisorApp.Contracts.V1.Advisor;
using Apexa.AdvisorApp.Domain.Entities;
using AutoMapper;

namespace Apexa.AdvisorApp.WebApi.Mappings
{
    public class ApiMapperProfile : Profile
    {
        public ApiMapperProfile()
        {
            CreateMap<CreateAdvisorApiRequest, CreateAdvisorCommand>();
            CreateMap<AdvisorWithPagingApiRequest, GetAdvisorsListQuery>();
            CreateMap<Advisor, AdvisorApiResponse>();
            


        }
    }
}
