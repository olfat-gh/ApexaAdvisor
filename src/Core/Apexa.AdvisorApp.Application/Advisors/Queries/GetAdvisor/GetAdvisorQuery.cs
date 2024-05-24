using Apexa.AdvisorApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apexa.AdvisorApp.Application.Advisors.Queries.GetAdvisor
{
    public class GetAdvisorQuery : IRequest<Advisor>
    {
        public Guid AdvisorId { get; set; }
    }
}
