

namespace Apexa.AdvisorApp.Contracts.V1.Advisor
{
    public class CreateAdvisorApiRequest
    {
        public required string Name { get; set; }
        public required string Sin { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
