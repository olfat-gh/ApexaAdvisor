using Apexa.AdvisorApp.Application.Common.Exceptions;
using Apexa.AdvisorApp.Application.Common.Interfaces;
using Apexa.AdvisorApp.Domain.Entities;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apexa.AdvisorApp.Application.Advisors.Commands.UpdateAdvisor
{
    public class UpdateAdvisorCommandHandler : IRequestHandler<UpdateAdvisorCommand>
    {
        private readonly IMapper _mapper;
        private readonly IAdvisorRepository _advisorRepository;

        public UpdateAdvisorCommandHandler(IMapper mapper, IAdvisorRepository advisorRepository)
        {
            _mapper = mapper;
            _advisorRepository = advisorRepository;
        }
        public async Task Handle(UpdateAdvisorCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateAdvisorCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);


            var advisor = await _advisorRepository.GetByIdAsync(request.Id);

            if (advisor == null)
            {
                throw new AdvisorNotFoundException(request.Id);
            }
            var advisorToUpdate = _mapper.Map(request, advisor);

            await _advisorRepository.UpdateAsync(advisorToUpdate);
        }
    }
}
