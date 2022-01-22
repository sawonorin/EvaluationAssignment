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
                //.ForMember(d => d.FreeDays, s => { s.MapFrom(e => e.FreeDays); })
                //.ForMember(d => d.Name, s => { s.MapFrom(e => e.Name); })
                //.ForMember(d => d.Start, s => { s.MapFrom(e => e.Start); })
                .ReverseMap();
            #endregion

            #region Customer Discount
            CreateMap<CustomerDiscountModel, CustomerDiscount>()
                //.ForMember(d => d.CustomerId, s => { s.MapFrom(e => e.CustomerId); })
                //.ForMember(d => d.Discount, s => { s.MapFrom(e => e.Discount); })
                //.ForMember(d => d.End, s => { s.MapFrom(e => e.End); })
                //.ForMember(d => d.Service, s => { s.MapFrom(e => e.Service); })
                //.ForMember(d => d.Start, s => { s.MapFrom(e => e.Start); })
                .ReverseMap();
            #endregion

            #region Customer Service
            CreateMap<CustomerServiceModel, CustomerService>()
                //.ForMember(d => d.CustomerId, s => { s.MapFrom(e => e.CustomerId); })
                //.ForMember(d => d.Price, s => { s.MapFrom(e => e.Price); })
                //.ForMember(d => d.Service, s => { s.MapFrom(e => e.Service); })
                //.ForMember(d => d.Start, s => { s.MapFrom(e => e.Start); })
                .ReverseMap();
            #endregion

            #region Customer Service
            CreateMap<BaseCostModel, BaseCost>()
                //.ForMember(d => d.Monday, s => { s.MapFrom(e => e.Monday); })
                //.ForMember(d => d.Tuesday, s => { s.MapFrom(e => e.Tuesday); })
                //.ForMember(d => d.Wednesday, s => { s.MapFrom(e => e.Wednesday); })
                //.ForMember(d => d.Thursday, s => { s.MapFrom(e => e.Thursday); })
                //.ForMember(d => d.Friday, s => { s.MapFrom(e => e.Friday); })
                //.ForMember(d => d.Saturday, s => { s.MapFrom(e => e.Saturday); })
                //.ForMember(d => d.Sunday, s => { s.MapFrom(e => e.Sunday); })
                .ReverseMap();
            #endregion
        }
    }
}
