using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apexa.AdvisorApp.Contracts.Common
{
    public class ErrorApiResponse
    {
        public int StatusCode { get; set; }
        public string FailedMessage { get; set; }
    }
}
