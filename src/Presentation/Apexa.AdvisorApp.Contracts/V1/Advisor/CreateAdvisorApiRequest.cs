

namespace Apexa.AdvisorApp.Contracts.V1.Advisor
{
    public class CreateAdvisorApiRequest
    {
        public string Name { get; set; }
        public string SIN { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
    }
}
