using Apexa.AdvisorApp.Application.Advisors.Commands.CreateAdvisor;
using Apexa.AdvisorApp.Application.Advisors.Commands.DeleteAdvisor;
using Apexa.AdvisorApp.Application.Advisors.Commands.UpdateAdvisor;
using Apexa.AdvisorApp.Application.Advisors.Queries.GetAdvisor;
using Apexa.AdvisorApp.Application.Advisors.Queries.GetAdvisorList;
using Apexa.AdvisorApp.Contracts.Common;
using Apexa.AdvisorApp.Contracts.V1.Advisor;
using Apexa.AdvisorApp.Domain.Entities;
using Asp.Versioning;
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
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateAdvisorApiRequest request)
        {
            _logger.LogInformation("Started Create endpoint. ");

            var command = _mapper.Map<CreateAdvisorCommand>(request);
            var id = await _mediator.Send(command);
            return Ok(id);
        }


        [HttpDelete("{advisorId:guid}", Name = "DeleteAdvisor")]
        [MapToApiVersion("1.0")]
        public async Task<IActionResult> Delete(Guid advisorId)
        {
            _logger.LogInformation("Started deleting endpoint for advisor with Id : {Id} . ", advisorId);

            var command = new DeleteAdvisorCommand() { AdvisorId = advisorId };
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpGet("{advisorId:guid}", Name = "GetAdvisorById")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<AdvisorApiResponse>> GetAdvisorById(Guid advisorId)
        {
            _logger.LogInformation("Started getting endpoint for advisor with Id : {Id} . ", advisorId);

            var command = new GetAdvisorQuery() { AdvisorId = advisorId };
            var advisor=await _mediator.Send(command);
            return Ok(_mapper.Map<AdvisorApiResponse>(advisor));
        }

        [HttpGet("List", Name = "GetAllAdvisors")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<PaginationApiResponse<AdvisorApiResponse>>> GetAllAdvisors([FromQuery] AdvisorWithPagingApiRequest request)
        {
            _logger.LogInformation("Started getting all advisors endpoint. ");

            var query = _mapper.Map<GetAdvisorsListQuery>(request);
            var (total,listAdvisors) = await _mediator.Send(query);

            return Ok(new PaginationApiResponse<AdvisorApiResponse>(total, _mapper.Map<List<AdvisorApiResponse>>(listAdvisors)));
        }

        [HttpPut(Name = "UpdateAdvisor")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult> Update([FromBody] UpdateAdvisorApiRequest request)
        {
            _logger.LogInformation("Started updating endpoint for advisor with Id : {Id} . ", request.Id);

            var command = _mapper.Map<UpdateAdvisorCommand>(request);
            await _mediator.Send(command);

            return NoContent();
        }
    }
}
