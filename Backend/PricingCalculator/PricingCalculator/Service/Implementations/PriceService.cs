using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PricingService.DataAccess.Entities;
using PricingService.DataAccess.Interfaces;
using PricingService.Domain.Interfaces;
using PricingService.Domain.Models;

namespace PricingService.Service.Implementations
{
    public class PriceService : IPriceManager
    {
        private readonly IMapper _mapper;
        private readonly IPriceRepository _priceRepository;
        private readonly ICustomerRepository _customerRepository;
        public PriceService(IMapper mapper, IPriceRepository priceRepository, ICustomerRepository customerRepository)
        {
            _mapper = mapper;
            _priceRepository = priceRepository;
            _customerRepository = customerRepository;
        }
        public CustomerDiscountModel AddCustomerDiscount(CustomerDiscountModel customerDiscount)
        {
            var addDiscount = _priceRepository.AddCustomerDiscount(_mapper.Map<CustomerDiscount>(customerDiscount));
            return customerDiscount;
        }

        public CustomerServiceModel AddCustomerService(CustomerServiceModel customerService)
        {
            var addService = _priceRepository.AddCustomerPrice(_mapper.Map<DataAccess.Entities.CustomerService>(customerService));
            return customerService;
        }

        public CustomerDiscountModel EditCustomerDiscount(CustomerDiscountModel customerDiscount)
        {
            var getExistingDiscount = _priceRepository.GetCustomerDiscountById(customerDiscount.Id);
            if (getExistingDiscount == null)
            {
                throw new ApplicationException("Discount does not exists");
            }
            getExistingDiscount.Discount = customerDiscount.Discount;
            getExistingDiscount.End = customerDiscount.End ?? getExistingDiscount.End;
            getExistingDiscount.Start = customerDiscount.Start ?? getExistingDiscount.Start;
            getExistingDiscount.Service = (int)customerDiscount.Service;
            getExistingDiscount.CustomerId = customerDiscount.CustomerId;

            var editDiscount = _priceRepository.UpdateCustomerDiscount(getExistingDiscount);
            return customerDiscount;
        }

        public CustomerServiceModel EditCustomerService(CustomerServiceModel customerService)
        {
            var getExistingService = _priceRepository.GetCustomerPriceById(customerService.Id);
            if (getExistingService == null)
            {
                throw new ApplicationException("Price does not exists");
            }
            getExistingService.Start = customerService.Start;
            getExistingService.Service = (int)customerService.Service;
            getExistingService.Price = customerService.Price ?? getExistingService.Price;
            getExistingService.CustomerId = customerService.CustomerId;

            var editService = _priceRepository.UpdateCustomerPrice(getExistingService);
            return customerService;
        }

        public CustomerDiscountModel GetCustomerDiscount(Guid customerDiscountId)
        {
            var getDiscount = _priceRepository.GetCustomerDiscountById(customerDiscountId);
            return _mapper.Map<CustomerDiscountModel>(getDiscount);
        }

        public CustomerServiceModel GetCustomerService(Guid customerServiceId)
        {
            var getBaseCost = _priceRepository.GetCustomerPriceById(customerServiceId);
            return _mapper.Map<CustomerServiceModel>(getBaseCost);
        }

        public decimal GetCustomerPricing(PriceQuery priceQuery)
        {
            //Get customer
            var customer = _customerRepository.GetCustomerById(priceQuery.CustomerId);
            if (customer == null)
            {
                throw new ArgumentException("Customer does not exist.");
            }
            //Get base cost
            var baseCost = _priceRepository.GetBaseCostbyService((int)priceQuery.Service);
            //Get unique price
            var customerUniquePrice = _priceRepository.GetCustomerPriceByService(priceQuery.CustomerId, (int)priceQuery.Service);
            if (customerUniquePrice == null)
            {
                throw new ApplicationException("Service not registered for customer");
            }
            //get discount
            var customerDiscount = _priceRepository.GetCustomerDiscountByService(priceQuery.CustomerId, (int)priceQuery.Service);

            DateTime billingStartDate = customer.Start.AddDays(customer.FreeDays);
            //compare frreday with end date.
            if (billingStartDate >= priceQuery.End)
            {
                return 0;
            }
            return GetPrice(baseCost, billingStartDate, priceQuery, customerUniquePrice.First(), customerDiscount.FirstOrDefault());
        }

        private Dictionary<string, bool> LoadBillableDays(BaseCost baseCost)
        {
            //get billable weekdays
            Dictionary<string, bool> billableWeekdays = new Dictionary<string, bool>();

            billableWeekdays.Add(DayOfWeek.Monday.ToString(), baseCost.Monday);
            billableWeekdays.Add(DayOfWeek.Tuesday.ToString(), baseCost.Tuesday);
            billableWeekdays.Add(DayOfWeek.Wednesday.ToString(), baseCost.Wednesday);
            billableWeekdays.Add(DayOfWeek.Thursday.ToString(), baseCost.Thursday);
            billableWeekdays.Add(DayOfWeek.Friday.ToString(), baseCost.Friday);
            billableWeekdays.Add(DayOfWeek.Saturday.ToString(), baseCost.Saturday);
            billableWeekdays.Add(DayOfWeek.Sunday.ToString(), baseCost.Sunday);

            return billableWeekdays;
        }

        private decimal GetPrice(BaseCost baseCost, DateTime? billingStartDate, PriceQuery priceQuery, DataAccess.Entities.CustomerService customerUniquePrice, CustomerDiscount customerDiscount)
        {
            decimal totalServicePrice = 0m;
            
            //get billable weekdays
            Dictionary<string, bool> billableWeekdays = LoadBillableDays(baseCost);

            //calculate free days end date
            billingStartDate = priceQuery.Start == null ? billingStartDate : billingStartDate >= priceQuery.Start ? billingStartDate : priceQuery?.Start;

            int? billableDays = (priceQuery.End - billingStartDate)?.Days;
            int counter = 0;
            while (counter <= billableDays)
            {
                //get day of week
                var currentDate = billingStartDate?.AddDays(counter);
                if (billableWeekdays[currentDate?.DayOfWeek.ToString()])
                {
                    decimal servicePrice = customerUniquePrice.Price ?? baseCost.Cost;
                    if (customerDiscount != null && (currentDate >= (customerDiscount.Start??DateTime.MinValue)) && (currentDate <= (customerDiscount.End??DateTime.MaxValue)))
                    {
                        servicePrice = (servicePrice * (100 - customerDiscount.Discount))/100;
                    }
                    totalServicePrice += servicePrice;
                }
                counter++;
            }
            return totalServicePrice;
        }

        public BaseCostModel AddBaseCost(BaseCostModel baseCost)
        {
            var addBaseCost = _priceRepository.AddBaseCost(_mapper.Map<DataAccess.Entities.BaseCost>(baseCost));
            return baseCost;
        }

        public BaseCostModel EditBaseCost(BaseCostModel baseCost)
        {
            var getExistingBaseCost = _priceRepository.GetBaseCostbyId(baseCost.Id);
            if (getExistingBaseCost == null)
            {
                throw new ApplicationException("Price does not exists");
            }
            getExistingBaseCost.Start = baseCost.Start;
            getExistingBaseCost.Service = (int)baseCost.Service;
            getExistingBaseCost.Cost = baseCost.Cost;
            getExistingBaseCost.Friday = baseCost.Friday;
            getExistingBaseCost.Monday = baseCost.Monday;
            getExistingBaseCost.Saturday = baseCost.Saturday;
            getExistingBaseCost.Sunday = baseCost.Sunday;
            getExistingBaseCost.Thursday = baseCost.Thursday;
            getExistingBaseCost.Tuesday = baseCost.Tuesday;
            getExistingBaseCost.Wednesday = baseCost.Wednesday;

            var editBaseCostt = _priceRepository.UpdateBaseCost(getExistingBaseCost);
            return baseCost;
        }

        public BaseCostModel GetBaseCost(Guid baseCostId)
        {
            var getBaseCost = _priceRepository.GetBaseCostbyId(baseCostId);
            return _mapper.Map<BaseCostModel>(getBaseCost);
        }
    }
}
