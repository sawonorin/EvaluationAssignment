using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PricingService.DataAccess.Entities;
using PricingService.DataAccess.Interfaces;

namespace PricingService.DataAccess.Implementations
{
    public class InMemCustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;
        public InMemCustomerRepository(AppDbContext context)
        {
            _context = context;
        }
        public Guid Add(Customer customer)
        {
            _context.Add(customer);
            _context.SaveChanges();
            return customer.Id;
        }

        public Customer GetCustomerById(Guid customerId)
        {
            return _context.Customers.Find(customerId);
        }

        public Guid Update(Customer customer)
        {
            _context.Update(customer);
            _context.SaveChanges();
            return customer.Id;
        }
    }
}
