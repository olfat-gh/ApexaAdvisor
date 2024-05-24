using Microsoft.AspNetCore.Mvc;

namespace Apexa.App.Advisor.WebApi.Controllers.V1
{
    [V1]
    public class AdvisorController : ApexaControllerBase
    {
        private readonly ILogger<AdvisorController> _logger;

        public AdvisorController(ILogger<AdvisorController> logger) : base(logger)
        {
            _logger = logger;
        }

        
    }
}
