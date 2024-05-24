using Apexa.AdvisorApp.Application.Advisors.Queries.GetAdvisorList;
using Apexa.AdvisorApp.Application.Common.Exceptions;
using Apexa.AdvisorApp.Application.Common.Interfaces;
using Apexa.AdvisorApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apexa.AdvisorApp.Application.Advisors.Queries.GetAdvisor
{
    public class GetAdvisorQueryHandler : IRequestHandler<GetAdvisorQuery, Advisor>
    {
        private readonly IAdvisorRepository _advisorRepository;
        public GetAdvisorQueryHandler(IAdvisorRepository advisorRepository)
        {
            _advisorRepository = advisorRepository;
        }
        public async Task<Advisor> Handle(GetAdvisorQuery request, CancellationToken cancellationToken)
        {
            var advisor = await _advisorRepository.GetByIdAsync(request.AdvisorId);

            if (advisor == null)
            {
                throw new AdvisorNotFoundException(request.AdvisorId);
            }

            return advisor;

        }
    }
}
