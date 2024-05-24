using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apexa.AdvisorApp.Application.Advisors.Commands.DeleteAdvisor
{
    public class DeleteAdvisorCommand: IRequest
    {
        public Guid AdvisorId { get; set; }
    }
}
