using Apexa.AdvisorApp.Application.Advisors.Queries.GetAdvisorList;
using Apexa.AdvisorApp.Application.Common.Interfaces;
using Apexa.AdvisorApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apexa.AdvisorApp.Application.Advisors.Queries.GetAdvisorsList
{
    public class GetAdvisorsListQueryHandler : IRequestHandler<GetAdvisorsListQuery, (int, List<Advisor>)>
    {

        private readonly IAdvisorRepository _advisorRepository;

        public GetAdvisorsListQueryHandler(IAdvisorRepository advisorRepository)
        {
            _advisorRepository = advisorRepository;
        }
        public async Task<(int,List<Advisor>)> Handle(GetAdvisorsListQuery request, CancellationToken cancellationToken)
        {

            return await _advisorRepository.GetAdvisorsAsync(request.PageIndex, request.PageSize);

        }
    }
}
