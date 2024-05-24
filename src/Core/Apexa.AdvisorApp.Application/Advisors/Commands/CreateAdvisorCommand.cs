using Apexa.App.Advisor.Domain.Enumerations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apexa.AdvisorApp.Application.Advisors.Commands
{
    public class CreateAdvisorCommand : IRequest<Guid>
    {
        public required string Name { get; set; }
        public required string Sin { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public HealthStatus Status { get; set; }

    }
}
