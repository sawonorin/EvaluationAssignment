using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PricingService.Domain.Models;

namespace PricingService.Domain.Interfaces
{
    public interface ICustomerManager
    {
        Guid RegisterCustomer(CustomerModel customer);
    }
}
