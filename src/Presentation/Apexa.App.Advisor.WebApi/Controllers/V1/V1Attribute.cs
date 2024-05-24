using Asp.Versioning;

namespace Apexa.App.Advisor.WebApi.Controllers.V1
{
    public class V1Attribute : ApiVersionAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public V1Attribute() : base(new ApiVersion(1, 0))
        {
        }
    }
}
