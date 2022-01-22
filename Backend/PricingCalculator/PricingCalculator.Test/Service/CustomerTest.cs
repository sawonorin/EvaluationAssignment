using System;
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PricingService.DataAccess;
using PricingService.DataAccess.Implementations;
using PricingService.DataAccess.Interfaces;
using PricingService.Domain.Interfaces;
using PricingService.Domain.Models;
using PricingService.Service.Implementations;
using PricingService.Service.Mappings;

namespace PricingService.Test.Service
{
    [TestClass]
    public class CustomerTest
    {
        private static IMapper _mapper;
        private static ICustomerRepository _customerRepository;

        public CustomerTest()
        {
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new AutoMapping());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }

            // Build DbContextOptions
            DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "PricingServiceTest")
                .Options;
            var TestContext = new AppDbContext(dbContextOptions);

            _customerRepository = new InMemCustomerRepository(TestContext);
        }

        [TestMethod]
        public void ShouldAddCustomer()
        {
            // Arrange            
            var logger = new Mock<ILogger<ICustomerManager>>();

            var newCustomer = new CustomerModel
            {
                CustomerId = Guid.NewGuid(),
                FreeDays = 20,
                Name = "Add Test",
                Start = DateTime.Now
            };

            // System under Test
            CustomerService sut = new CustomerService(_mapper, _customerRepository);

            // Act
            var newGuid = sut.RegisterCustomer(newCustomer);

            //Assert
            newGuid.Should().Be(newCustomer.CustomerId);
        }
    }
}
