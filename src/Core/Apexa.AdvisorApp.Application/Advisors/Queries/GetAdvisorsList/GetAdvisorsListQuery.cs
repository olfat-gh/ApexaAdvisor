using Apexa.AdvisorApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apexa.AdvisorApp.Application.Advisors.Queries.GetAdvisorList
{
    public class GetAdvisorsListQuery : IRequest<(int,List<Advisor>)> 
    {
        public int PageSize { get; set; } 
        public int PageIndex { get; set; } 
    }
}
