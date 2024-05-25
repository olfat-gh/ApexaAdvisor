using Apexa.AdvisorApp.Application.Advisors.Queries.GetAdvisorList;
using Apexa.AdvisorApp.Application.Common.Exceptions;
using Apexa.AdvisorApp.Application.Common.Interfaces;
using Apexa.AdvisorApp.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apexa.AdvisorApp.Application.Advisors.Queries.GetAdvisor
{
    public class GetAdvisorQueryHandler : IRequestHandler<GetAdvisorQuery, Advisor>
    {
        private readonly ILogger<GetAdvisorQueryHandler> _logger;
        private readonly IAdvisorRepository _advisorRepository;
        public GetAdvisorQueryHandler(ILogger<GetAdvisorQueryHandler> logger, IAdvisorRepository advisorRepository)
        {
            _logger = logger;
            _advisorRepository = advisorRepository;
        }
        public async Task<Advisor> Handle(GetAdvisorQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting Advisor with Id : {Id}", request.AdvisorId);

            var advisor = await _advisorRepository.GetByIdAsync(request.AdvisorId);

            if (advisor == null)
            {
                _logger.LogWarning("Advisor with Id : {Id} not found.", request.AdvisorId);

                throw new AdvisorNotFoundException(request.AdvisorId);
            }

            return advisor;

        }
    }
}
