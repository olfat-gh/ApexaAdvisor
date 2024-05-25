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

namespace Apexa.AdvisorApp.Application.Advisors.Commands.UpdateAdvisor
{
    public class UpdateAdvisorCommandHandler : IRequestHandler<UpdateAdvisorCommand>
    {
        private readonly ILogger<UpdateAdvisorCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IAdvisorRepository _advisorRepository;

        public UpdateAdvisorCommandHandler(ILogger<UpdateAdvisorCommandHandler> logger,IMapper mapper, IAdvisorRepository advisorRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _advisorRepository = advisorRepository;
        }
        public async Task Handle(UpdateAdvisorCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Started updating Advisor with Id : {Id} by validating ...", request.Id);
            var validator = new UpdateAdvisorCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0) 
            {
                _logger.LogWarning("Cannot validate the data.");
                throw new ValidationException(validationResult);
            }

            _logger.LogInformation("Getting Advisor with Id : {Id}", request.Id);

            var advisor = await _advisorRepository.GetByIdAsync(request.Id);

            if (advisor == null)
            {
                _logger.LogWarning("Advisor with Id : {Id} not found.", request.Id);

                throw new AdvisorNotFoundException(request.Id);
            }
            var advisorToUpdate = _mapper.Map(request, advisor);

            _logger.LogInformation("Updating Advisor with Id : {Id}", request.Id);

            await _advisorRepository.UpdateAsync(advisorToUpdate);
        }
    }
}
