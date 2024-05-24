using Microsoft.AspNetCore.Mvc;

namespace Apexa.AdvisorApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApexaControllerBase : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        protected readonly ILogger Logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        protected ApexaControllerBase(ILogger logger) => Logger = logger;
    }
}
