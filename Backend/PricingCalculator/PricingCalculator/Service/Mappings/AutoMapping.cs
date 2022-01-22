using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PricingService.DataAccess.Entities;
using PricingService.Domain.Models;

namespace PricingService.Service.Mappings
{
    public class AutoMapping : Profile
    {
        /// <summary>
        /// Constructor containing mapping rules.
        /// </summary>
        public AutoMapping()
        {
            AllowNullCollections = true;

            #region Customer
            CreateMap<CustomerModel, Customer>()
                .ForMember(d => d.Id, s => { s.MapFrom(e => e.CustomerId); })
                .ReverseMap();
            #endregion

            #region Customer Discount
            CreateMap<CustomerDiscountModel, CustomerDiscount>()
                .ReverseMap();
            #endregion

            #region Customer Service
            CreateMap<CustomerServiceModel, CustomerService>()
                .ReverseMap();
            #endregion

            #region Customer Service
            CreateMap<BaseCostModel, BaseCost>()
                .ReverseMap();
            #endregion
        }
    }
}
