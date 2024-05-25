using Apexa.AdvisorApp.Application.Common.Exceptions;
using Apexa.AdvisorApp.Application.Common.Interfaces;
using Apexa.AdvisorApp.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apexa.AdvisorApp.Application.Advisors.Commands.CreateAdvisor
{
    public class CreateAdvisorCommandHandler : IRequestHandler<CreateAdvisorCommand, Guid>
    {
        private readonly ILogger<CreateAdvisorCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IAdvisorRepository _advisorRepository;

        public CreateAdvisorCommandHandler(ILogger<CreateAdvisorCommandHandler> logger, IMapper mapper, IAdvisorRepository advisorRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _advisorRepository = advisorRepository;
        }
        public async Task<Guid> Handle(CreateAdvisorCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started adding new Advisor by validating...");
            var validator = new CreateAdvisorCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                _logger.LogWarning("Cannot validate the data.");
                throw new ValidationException(validationResult);
            }

            _logger.LogInformation("Adding new Advisor to DB...");

            var advisor = _mapper.Map<Advisor>(request);
            advisor = await _advisorRepository.AddAsync(advisor);

            return advisor.Id;


        }


    }
}
