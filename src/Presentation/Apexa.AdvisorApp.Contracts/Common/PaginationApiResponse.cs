using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apexa.AdvisorApp.Contracts.Common
{
    public class PaginationApiResponse<T> where T : class
    {
        public PaginationApiResponse(int totalPages, IList<T> records)
        {
            TotalPages = totalPages;
            Records = records;
        }

        public int TotalPages { get; set; }
        public IList<T> Records { get; set; }
    }
}
