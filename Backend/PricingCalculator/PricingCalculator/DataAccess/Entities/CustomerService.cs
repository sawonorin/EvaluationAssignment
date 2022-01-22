using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PricingService.DataAccess.Entities
{
    public class CustomerService : BaseEntity
    {
        public Guid CustomerId { get; set; }
        public int Service { get; set; }
        public DateTime Start { get; set; }
        public decimal? Price { get; set; }
        public Customer Customer { get; set; }
    }
}
