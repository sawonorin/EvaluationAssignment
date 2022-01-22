using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PricingService.Domain.Interfaces;
using PricingService.Domain.Models;

namespace PricingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerManager _customerManager;

        public CustomerController(ILogger<CustomerController> logger, ICustomerManager customerManager)
        {
            _logger = logger;
            _customerManager = customerManager;
        }

        [HttpPost]
        public IActionResult Post(CustomerModel customer)
        {
            return Ok(_customerManager.RegisterCustomer(customer));
        }
    }
}
