using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PricingService.Domain.Models
{
    public sealed class CustomerModel
    {
        public Guid CustomerId { get; set; }
        public string Name { get; set; }
        public int FreeDays { get; set; }
        public DateTime Start { get; set; }
    }
}
