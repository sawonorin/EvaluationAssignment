using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PricingService.DataAccess.Entities;
using PricingService.DataAccess.Interfaces;

namespace PricingService.DataAccess.Implementations
{
    public class InMemPriceRepository : IPriceRepository
    {
        private readonly AppDbContext _context;
        public InMemPriceRepository(AppDbContext context)
        {
            _context = context;
        }
        public Guid AddBaseCost(BaseCost baseCost)
        {
            _context.Add(baseCost);
            _context.SaveChanges();
            return baseCost.Id;
        }

        public Guid AddCustomerDiscount(CustomerDiscount customerDiscount)
        {
            _context.Add(customerDiscount);
            _context.SaveChanges();
            return customerDiscount.Id;
        }

        public Guid AddCustomerPrice(CustomerService customerService)
        {
            _context.Add(customerService);
            _context.SaveChanges();
            return customerService.Id;
        }

        public BaseCost GetBaseCostbyId(Guid baseCostId)
        {
            return _context.BaseCosts.Find(baseCostId);
        }

        public BaseCost GetBaseCostbyService(int Service)
        {
            return _context.BaseCosts.Where(e => e.Service.Equals(Service)).First();
        }

        public IQueryable<CustomerDiscount> GetCustomerDiscountByService(Guid customerId, int Service)
        {
            return _context.CustomerDiscounts.Where(e => e.Service.Equals(Service) && e.CustomerId.Equals(customerId));
        }

        public CustomerDiscount GetCustomerDiscountById(Guid discountId)
        {
            return _context.CustomerDiscounts.Find(discountId);
        }

        public IQueryable<CustomerService> GetCustomerPriceByService(Guid customerId, int Service)
        {
            return _context.CustomerServices.Where(e => e.Service.Equals(Service) && e.CustomerId.Equals(customerId));
        }

        public CustomerService GetCustomerPriceById(Guid priceId)
        {
            return _context.CustomerServices.Find(priceId);
        }

        public Guid UpdateBaseCost(BaseCost baseCost)
        {
            _context.Update(baseCost);
            _context.SaveChanges();
            return baseCost.Id;
        }

        public Guid UpdateCustomerDiscount(CustomerDiscount customerDiscount)
        {
            _context.Update(customerDiscount);
            _context.SaveChanges();
            return customerDiscount.Id;
        }

        public Guid UpdateCustomerPrice(CustomerService customerService)
        {
            _context.Update(customerService);
            _context.SaveChanges();
            return customerService.Id;
        }
    }
}
