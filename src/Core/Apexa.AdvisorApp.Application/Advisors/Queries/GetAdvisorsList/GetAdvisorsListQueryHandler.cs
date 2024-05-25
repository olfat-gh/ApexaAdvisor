using Apexa.AdvisorApp.Application.Advisors.Queries.GetAdvisorList;
using Apexa.AdvisorApp.Application.Common.Interfaces;
using Apexa.AdvisorApp.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apexa.AdvisorApp.Application.Advisors.Queries.GetAdvisorsList
{
    public class GetAdvisorsListQueryHandler : IRequestHandler<GetAdvisorsListQuery, (int, List<Advisor>)>
    {
        private readonly ILogger<GetAdvisorsListQueryHandler> _logger;
        private readonly IAdvisorRepository _advisorRepository;

        public GetAdvisorsListQueryHandler(ILogger<GetAdvisorsListQueryHandler> logger, IAdvisorRepository advisorRepository)
        {
            _logger = logger;
            _advisorRepository = advisorRepository;
        }
        public async Task<(int,List<Advisor>)> Handle(GetAdvisorsListQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting All Advisors... ");

            return await _advisorRepository.GetAdvisorsAsync(request.PageIndex, request.PageSize);

        }
    }
}
