using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apexa.AdvisorApp.Contracts.V1.Advisor
{
    public class UpdateAdvisorApiRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SIN { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public HealthStatus Status { get; set; }
    }
}
