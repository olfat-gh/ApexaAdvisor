using Apexa.App.Advisor.Domain.Enumerations;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apexa.AdvisorApp.Domain.Entities
{
    public class Advisor
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Sin { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public HealthStatus Status { get; set; }

        public HealthStatus GenerateRandomHealthStatus()
        {
            var random = new Random();
            return random.Next(100) switch
            {
                < 60 => HealthStatus.Green,
                < 80 => HealthStatus.Yellow,
                _ => HealthStatus.Red

            };
        }



    }
}
