using System.ComponentModel.Design;

namespace Apexa.AdvisorApp.Application.Common.Exceptions
{
    public class AdvisorNotFoundException : NotFoundException
    {
        public AdvisorNotFoundException(Guid AdvisorId) :
            base($"The Advisor with id: {AdvisorId} doesn't exist.")
        {
        }
    }
}
