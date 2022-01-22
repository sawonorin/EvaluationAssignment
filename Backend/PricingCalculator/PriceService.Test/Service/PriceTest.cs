using System;
using FluentAssertions;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PricingService.DataAccess.Interfaces;
using PricingService.Domain.Interfaces;
using PricingService.Domain.Models;
using PricingService.Service.Implementations;
using PricingService.Service.Mappings;
using Microsoft.EntityFrameworkCore;
using PricingService.DataAccess;
using PricingService.DataAccess.Implementations;

namespace PricingService.Test.Service
{
    [TestClass]
    public class PriceTest
    {
        private static IMapper _mapper;
        private static ICustomerRepository _customerRepository;
        private static IPriceRepository _priceRepository;

        public PriceTest()
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
            _priceRepository = new InMemPriceRepository(TestContext);
        }

        #region BaseCost

        [TestMethod]
        public void ShouldAddBaseCost()
        {
            // Arrange
            var logger = new Mock<ILogger<IPriceManager>>();

            var baseCost = new BaseCostModel
            {
                Cost = 10,
                Friday = true,
                Monday = true,
                Tuesday = true,
                Thursday = true,
                Wednesday = true,
                Start = DateTime.Now,
                Service = Domain.Models.Service.Service_A
            };

            // System under Test

            PriceService sut = new PriceService(_mapper, _priceRepository, _customerRepository);

            // Act
            var newBaseCost = sut.AddBaseCost(baseCost);

            //Assert
            baseCost.Should().Be(newBaseCost);
        }

        [TestMethod]
        public void ShouldEditBaseCost()
        {
            // Arrange
            var logger = new Mock<ILogger<IPriceManager>>();

            var baseCost = new BaseCostModel
            {
                Cost = 10,
                Friday = true,
                Monday = true,
                Tuesday = true,
                Thursday = true,
                Wednesday = true,
                Start = DateTime.Now,
                Service = Domain.Models.Service.Service_A
            };

            // System under Test
            PriceService sut = new PriceService(_mapper, _priceRepository, _customerRepository);

            // Act
            var newBaseCost = sut.AddBaseCost(baseCost);

            newBaseCost.Cost = 0.10m;
            var editBaseCost = sut.EditBaseCost(newBaseCost);
            var getEditedBaseCost = sut.GetBaseCost(editBaseCost.Id);

            //Assert
            getEditedBaseCost.Cost.Should().Be(0.10m);
        }

        #endregion

        #region Customer Service

        [TestMethod]
        public void ShouldAddCustomerService()
        {
            // Arrange
            var logger = new Mock<ILogger<IPriceManager>>();

            var customerService = new CustomerServiceModel
            {
                CustomerId = Guid.NewGuid(),
                Price = 0.16m,
                Start = DateTime.Now,
                Service = Domain.Models.Service.Service_A
            };

            // System under Test
            PriceService sut = new PriceService(_mapper, _priceRepository, _customerRepository);

            // Act
            var newCustomerService = sut.AddCustomerService(customerService);

            //Assert
            customerService.Should().Be(newCustomerService);
        }

        [TestMethod]
        public void ShouldEditCustomerService()
        {
            // Arrange
            var logger = new Mock<ILogger<IPriceManager>>();

            var customerService = new CustomerServiceModel
            {
                CustomerId = Guid.NewGuid(),
                Price = 0.16m,
                Start = DateTime.Now,
                Service = Domain.Models.Service.Service_A
            };

            // System under Test

            PriceService sut = new PriceService(_mapper, _priceRepository, _customerRepository);

            // Act
            var newCustomerService = sut.AddCustomerService(customerService);

            newCustomerService.Price = 0.20m;
            var editCustomerService = sut.EditCustomerService(newCustomerService);
            var getEditCustomerService = sut.GetCustomerService(editCustomerService.Id);
            
            //Assert
            getEditCustomerService.Price.Should().Be(0.20m);
        }

        #endregion

        #region Customer Discount

        [TestMethod]
        public void ShouldAddCustomerDiscount()
        {
            // Arrange
            var logger = new Mock<ILogger<IPriceManager>>();

            var customerDiscount = new CustomerDiscountModel
            {
                CustomerId = Guid.NewGuid(),
                Start = DateTime.Now,
                Service = Domain.Models.Service.Service_A,
                Discount = 10,
                End = DateTime.Parse("2022/12/31")
            };

            // System under Test
            PriceService sut = new PriceService(_mapper, _priceRepository, _customerRepository);

            // Act
            var newCustomerDiscount = sut.AddCustomerDiscount(customerDiscount);

            //Assert
            customerDiscount.Should().Be(newCustomerDiscount);
        }

        [TestMethod]
        public void ShouldEditCustomerDiscount()
        {
            // Arrange
            var logger = new Mock<ILogger<IPriceManager>>();

            var customerDiscount = new CustomerDiscountModel
            {
                CustomerId = Guid.NewGuid(),
                Start = DateTime.Now,
                Service = Domain.Models.Service.Service_A,
                Discount = 10,
                End = DateTime.Parse("2022/12/31")
            };

            // System under Test
            PriceService sut = new PriceService(_mapper, _priceRepository, _customerRepository);

            // Act
            var newCustomerDiscount = sut.AddCustomerDiscount(customerDiscount);

            newCustomerDiscount.Discount = 40;
            var editCustomerDiscount = sut.EditCustomerDiscount(newCustomerDiscount);
            var getEditCustomerDiscount = sut.GetCustomerDiscount(editCustomerDiscount.Id);

            //Assert
            getEditCustomerDiscount.Discount.Should().Be(40m);
        }

        #endregion

        #region Customer Pricing
        /// <summary>
        /// _Customer X_ started using _Service A_ and _Service C_ 2019-09-20. _Customer X_ also had an discount of 20% between 2019-09-22 and 2019-09-24 for _Service C_. What is the total price for _Customer X_ up until 2019-10-01?
        /// </summary>
        [TestMethod]
        public void ShouldGetCustomerXPricing()
        {
            // Arrange
            var logger = new Mock<ILogger<IPriceManager>>();

            //The BaseCost
            var baseCostA = new BaseCostModel
            {
                Cost = 0.2m,
                Friday = true,
                Monday = true,
                Tuesday = true,
                Thursday = true,
                Wednesday = true,
                Start = DateTime.Parse("2019-09-20"),
                Service = Domain.Models.Service.Service_A
            };
            var baseCostC = new BaseCostModel
            {
                Cost = 0.4m,
                Friday = true,
                Monday = true,
                Tuesday = true,
                Thursday = true,
                Wednesday = true,
                Saturday = true,
                Sunday = true,
                Start = DateTime.Parse("2019-09-20"),
                Service = Domain.Models.Service.Service_C
            };
            //The Customer
            var newCustomer = new CustomerModel
            {
                CustomerId = Guid.NewGuid(),
                FreeDays = 0,
                Name = "_Customer X_",
                Start = DateTime.Parse("2019-09-20")
            };
            //The Services
            var customerServiceA = new CustomerServiceModel
            {
                CustomerId = newCustomer.CustomerId,
                Start = newCustomer.Start,
                Service = Domain.Models.Service.Service_A
            };
            var customerServiceC = new CustomerServiceModel
            {
                CustomerId = newCustomer.CustomerId,
                Start = newCustomer.Start,
                Service = Domain.Models.Service.Service_C
            };
            //The Discount
            var customerDiscountC = new CustomerDiscountModel
            {
                CustomerId = newCustomer.CustomerId,
                Start = DateTime.Parse("2019-09-22"),
                Service = Domain.Models.Service.Service_C,
                Discount = 20,
                End = DateTime.Parse("2019-09-24")
            };
            //Price Query
            var priceQuery = new PriceQuery
            {
                CustomerId = newCustomer.CustomerId,
                End = DateTime.Parse("2019-10-01"),
                Service = Domain.Models.Service.Service_C
            };

            // System under Test
            CustomerService sutCustomer = new CustomerService(_mapper, _customerRepository);
            PriceService sutPricing = new PriceService(_mapper, _priceRepository, _customerRepository);

            // Act
            var newBaseCostA = sutPricing.AddBaseCost(baseCostA);
            var newBaseCostC = sutPricing.AddBaseCost(baseCostC);

            var newCustomerX = sutCustomer.RegisterCustomer(newCustomer);

            var newCustomerServiceA = sutPricing.AddCustomerService(customerServiceA);
            var newCustomerServiceC = sutPricing.AddCustomerService(customerServiceC);

            var newCustomerDiscountC = sutPricing.AddCustomerDiscount(customerDiscountC);

            var getPricing = sutPricing.GetCustomerPricing(priceQuery);

            //Assert
            getPricing.Should().Be(4.56m);
        }

        /// <summary>
        /// _Customer Y_ started using _Service B_ and _Service C_ 2018-01-01. _Customer Y_ had 200 free days and a discount of 30% for the rest of the time. What is the total price for _Customer Y_ up until 2019-10-01?
        /// </summary>
        [TestMethod]
        public void ShouldGetCustomerYPricing()
        {
            // Arrange
            var logger = new Mock<ILogger<IPriceManager>>();

            //The BaseCost
            var baseCostB = new BaseCostModel
            {
                Cost = 0.24m,
                Friday = true,
                Monday = true,
                Tuesday = true,
                Thursday = true,
                Wednesday = true,
                Start = DateTime.Parse("2018-01-01"),
                Service = Domain.Models.Service.Service_B
            };
            var baseCostC = new BaseCostModel
            {
                Cost = 0.4m,
                Friday = true,
                Monday = true,
                Tuesday = true,
                Thursday = true,
                Wednesday = true,
                Saturday = true,
                Sunday = true,
                Start = DateTime.Parse("2018-01-01"),
                Service = Domain.Models.Service.Service_C
            };
            //The Customer
            var newCustomer = new CustomerModel
            {
                CustomerId = Guid.NewGuid(),
                FreeDays = 200,
                Name = "_Customer Y_",
                Start = DateTime.Parse("2018-01-01")
            };
            //The Services
            var customerServiceA = new CustomerServiceModel
            {
                CustomerId = newCustomer.CustomerId,
                Start = newCustomer.Start,
                Service = Domain.Models.Service.Service_B
            };
            var customerServiceC = new CustomerServiceModel
            {
                CustomerId = newCustomer.CustomerId,
                Start = newCustomer.Start,
                Service = Domain.Models.Service.Service_C
            };
            //The Discount
            var customerDiscountC = new CustomerDiscountModel
            {
                CustomerId = newCustomer.CustomerId,
                Service = Domain.Models.Service.Service_C,
                Discount = 30,
            };
            //Price Query
            var priceQueryB = new PriceQuery
            {
                CustomerId = newCustomer.CustomerId,
                End = DateTime.Parse("2019-10-01"),
                Service = Domain.Models.Service.Service_B
            };
            var priceQueryC = new PriceQuery
            {
                CustomerId = newCustomer.CustomerId,
                End = DateTime.Parse("2019-10-01"),
                Service = Domain.Models.Service.Service_C
            };

            // System under Test
            CustomerService sutCustomer = new CustomerService(_mapper, _customerRepository);
            PriceService sutPricing = new PriceService(_mapper, _priceRepository, _customerRepository);

            // Act
            var newBaseCostA = sutPricing.AddBaseCost(baseCostB);
            var newBaseCostC = sutPricing.AddBaseCost(baseCostC);

            var newCustomerX = sutCustomer.RegisterCustomer(newCustomer);

            var newCustomerServiceA = sutPricing.AddCustomerService(customerServiceA);
            var newCustomerServiceC = sutPricing.AddCustomerService(customerServiceC);

            var newCustomerDiscountC = sutPricing.AddCustomerDiscount(customerDiscountC);

            var getPricingB = sutPricing.GetCustomerPricing(priceQueryB);
            var getPricingC = sutPricing.GetCustomerPricing(priceQueryB);

            var totalPrice = getPricingB + getPricingC;

            //Assert
            totalPrice.Should().Be(150.24m);
        }

        #endregion
    }
}
