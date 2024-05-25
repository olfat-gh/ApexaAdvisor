using Apexa.AdvisorApp.Application.Common.Exceptions;
using Apexa.AdvisorApp.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apexa.AdvisorApp.Application.Advisors.Commands.DeleteAdvisor
{
    public class DeleteAdvisorCommandHandler : IRequestHandler<DeleteAdvisorCommand>
    {
        private readonly ILogger<DeleteAdvisorCommandHandler> _logger;
        private readonly IAdvisorRepository _advisorRepository;

        public DeleteAdvisorCommandHandler(ILogger<DeleteAdvisorCommandHandler> logger, IAdvisorRepository advisorRepository)
        {
            _logger = logger;
            _advisorRepository = advisorRepository;
        }
        public async Task Handle(DeleteAdvisorCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started removing Advisor with Id : {Id}", request.AdvisorId);
          
            var advisor = await _advisorRepository.GetByIdAsync(request.AdvisorId);

            if (advisor == null)
            {
                _logger.LogWarning("Advisor with Id : {Id} not found.", request.AdvisorId);

                throw new AdvisorNotFoundException(request.AdvisorId);
            }
            _logger.LogInformation("Removing Advisor with Id : {Id} from DB...", request.AdvisorId);

            await _advisorRepository.DeleteAsync(advisor);


        }
    }
}
