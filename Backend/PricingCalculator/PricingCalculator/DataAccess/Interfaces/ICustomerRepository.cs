using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PricingService.DataAccess.Entities;

namespace PricingService.DataAccess.Interfaces
{
    public interface ICustomerRepository
    {
        Customer GetCustomerById(Guid customerId);
        Guid Add(Customer customer);
        Guid Update(Customer customer);
    }
}
