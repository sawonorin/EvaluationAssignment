using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PricingService.DataAccess.Implementations;
using PricingService.DataAccess.Interfaces;

namespace PricingService.DataAccess
{
    public static class DataAccessExtensions
    {
        public static void AddDataServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("PricingService"));

            services.AddEntityFrameworkInMemoryDatabase();
            services.AddScoped<DbContext, AppDbContext>();

            //Repository
            services.AddScoped<IPriceRepository, InMemPriceRepository>();
            services.AddScoped<ICustomerRepository, InMemCustomerRepository>();
        }
    }
}
