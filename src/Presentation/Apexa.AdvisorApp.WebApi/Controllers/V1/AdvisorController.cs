using Apexa.AdvisorApp.Application.Advisors.Commands;
using Apexa.AdvisorApp.Contracts.V1.Advisor;
using Apexa.AdvisorApp.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Apexa.AdvisorApp.WebApi.Controllers.V1
{
    [V1]
    public class AdvisorController : ApexaControllerBase
    {
        private readonly ILogger<AdvisorController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AdvisorController(ILogger<AdvisorController> logger, IMediator mediator, IMapper mapper) : base(logger)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
        }


        [HttpPost(Name = "AddAdvisor")]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateAdvisorApiRequest payload)
        {
            var command = _mapper.Map<CreateAdvisorCommand>(payload);
            var id = await _mediator.Send(command);
            return Ok(id);
        }



    }
}
