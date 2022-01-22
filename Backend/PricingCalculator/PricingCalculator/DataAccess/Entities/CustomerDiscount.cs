using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PricingService.DataAccess.Entities
{
    public class CustomerDiscount : BaseEntity
    {
        public Guid CustomerId { get; set; }
        public int Service { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public decimal Discount { get; set; }
        public Customer Customer { get; set; }
    }
}
