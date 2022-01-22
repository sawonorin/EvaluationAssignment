using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PricingService.Domain.Models;

namespace PricingService.Domain.Interfaces
{
    public interface IPriceManager
    {
        BaseCostModel AddBaseCost(BaseCostModel baseCost);
        BaseCostModel EditBaseCost(BaseCostModel baseCost);
        CustomerServiceModel AddCustomerService(CustomerServiceModel customerService);
        CustomerServiceModel EditCustomerService(CustomerServiceModel customerService);
        CustomerDiscountModel AddCustomerDiscount(CustomerDiscountModel customerService);
        CustomerDiscountModel EditCustomerDiscount(CustomerDiscountModel customerService);
        decimal GetCustomerPricing(PriceQuery priceQuery);
    }
}
