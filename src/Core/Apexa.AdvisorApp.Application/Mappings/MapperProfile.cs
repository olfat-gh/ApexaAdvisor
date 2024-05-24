using Apexa.AdvisorApp.Application.Advisors.Commands;
using Apexa.AdvisorApp.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apexa.AdvisorApp.Application.Mappings
{
    public class MapperProfile : Profile
    {
        public MapperProfile() 
        {
            CreateMap<CreateAdvisorCommand, Advisor>();
        }
    }
}
