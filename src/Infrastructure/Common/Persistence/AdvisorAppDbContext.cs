using Apexa.AdvisorApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Apexa.AdvisorApp.Infrastructure.Common.Persistence
{
    public class AdvisorAppDbContext : DbContext
    {

        public AdvisorAppDbContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<Advisor> Advisors { get; set; }
    }
}
