using Apexa.AdvisorApp.Application.Common.Interfaces;
using Apexa.AdvisorApp.Domain.Entities;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apexa.AdvisorApp.Application.Advisors.Commands.CreateAdvisor
{
    public class CreateAdvisorCommandHandler : IRequestHandler<CreateAdvisorCommand, Guid>
    {
        private readonly IMapper _mapper;
        private readonly IAdvisorRepository _advisorRepository;

        public CreateAdvisorCommandHandler(IMapper mapper, IAdvisorRepository advisorRepository)
        {
            _mapper = mapper;
            _advisorRepository = advisorRepository;
        }
        public async Task<Guid> Handle(CreateAdvisorCommand request, CancellationToken cancellationToken)
        {
            var advisor = _mapper.Map<Advisor>(request);
            advisor = await _advisorRepository.AddAsync(advisor);

            return advisor.Id;


        }


    }
}
