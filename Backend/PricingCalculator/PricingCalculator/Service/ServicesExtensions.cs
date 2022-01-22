using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PricingService.DataAccess.Entities;
using PricingService.Domain.Interfaces;
using PricingService.Service.Implementations;
using CustomerService = PricingService.Service.Implementations.CustomerService;

namespace PricingService.Service
{
    public static class ServicesExtensions
    {
        public static void AddAppServices(this IServiceCollection services,
           IConfiguration Configuration)
        {
            services.AddAutoMapper(typeof(CustomerService));

            //Services
            services.AddScoped<ICustomerManager, CustomerService>();
            services.AddScoped<IPriceManager, PriceService>();
        }
    }
}
