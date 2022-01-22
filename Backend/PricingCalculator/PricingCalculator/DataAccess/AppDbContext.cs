using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PricingService.DataAccess.Entities;

namespace PricingService.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<BaseCost> BaseCosts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerService> CustomerServices { get; set; }
        public DbSet<CustomerDiscount> CustomerDiscounts { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseInMemoryDatabase("PricingService");
        //}
    }
}
