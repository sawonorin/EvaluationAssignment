using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PricingService.DataAccess.Entities;

namespace PricingService.DataAccess.Interfaces
{
    public interface IPriceRepository
    {
        Guid AddBaseCost(BaseCost baseCost);
        Guid UpdateBaseCost(BaseCost baseCost);
        BaseCost GetBaseCostbyId(Guid baseCostId);
        BaseCost GetBaseCostbyService(int Service);
        Guid AddCustomerPrice(CustomerService customerService);
        Guid UpdateCustomerPrice(CustomerService customerService);
        CustomerService GetCustomerPriceById(Guid priceId);
        IQueryable<CustomerService> GetCustomerPriceByService(Guid customerId, int Service);
        Guid AddCustomerDiscount(CustomerDiscount customerDiscount);
        Guid UpdateCustomerDiscount(CustomerDiscount customerDiscount);
        CustomerDiscount GetCustomerDiscountById(Guid discountId);
        IQueryable<CustomerDiscount> GetCustomerDiscountByService(Guid customerId, int Service);
    }
}
