using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PricingService.DataAccess.Entities
{
    public class Customer: BaseEntity
    {
        public string Name { get; set; }
        public int FreeDays { get; set; }
        public DateTime Start { get; set; }
        public ICollection<CustomerDiscount> CustomerDiscounts { get; set; } = new List<CustomerDiscount>();
        public ICollection<CustomerService> CustomerServices { get; set; } = new List<CustomerService>();
    }
}
