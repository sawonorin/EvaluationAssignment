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
    public class CustomerService : ICustomerManager
    {
        private readonly IMapper _mapper;
        private ICustomerRepository _customerRepository;
        public CustomerService(IMapper mapper, ICustomerRepository customerRepository)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
        }
        public Guid RegisterCustomer(CustomerModel customer)
        {
            var newCustomer = _mapper.Map<Customer>(customer);
            return _customerRepository.Add(newCustomer);
        }
    }
}
