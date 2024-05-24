using Apexa.AdvisorApp.Application.Common.Exceptions;
using Apexa.AdvisorApp.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apexa.AdvisorApp.Application.Advisors.Commands.DeleteAdvisor
{
    public class DeleteAdvisorCommandHandler : IRequestHandler<DeleteAdvisorCommand>
    {
        private readonly IAdvisorRepository _advisorRepository;

        public DeleteAdvisorCommandHandler(IAdvisorRepository advisorRepository)
        {
            _advisorRepository = advisorRepository;
        }
        public async Task Handle(DeleteAdvisorCommand request, CancellationToken cancellationToken)
        {
            var advisor = await _advisorRepository.GetByIdAsync(request.AdvisorId);

            if (advisor == null)
            {
                throw new AdvisorNotFoundException(request.AdvisorId);
            }
            await _advisorRepository.DeleteAsync(advisor);
        }
    }
}
