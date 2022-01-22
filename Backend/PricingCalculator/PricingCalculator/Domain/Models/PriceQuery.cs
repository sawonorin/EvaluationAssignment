using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PricingService.Domain.Models
{
    public sealed class PriceQuery
    {
        public Guid CustomerId { get; set; }
        public Service Service { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
    }
}
