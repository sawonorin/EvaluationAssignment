using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PricingService.Domain.Interfaces;
using PricingService.Domain.Models;

namespace PricingService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PriceController : Controller
    {
        private readonly ILogger<PriceController> _logger;
        private readonly IPriceManager _priceManager;

        public PriceController(ILogger<PriceController> logger, IPriceManager priceManager)
        {
            _logger = logger;
            _priceManager = priceManager;
        }

        [HttpPost]
        public IActionResult GetCustomerPrice(PriceQuery priceQuery)
        {
            return Ok(_priceManager.GetCustomerPricing(priceQuery));
        }

        [HttpPost("basecost")]
        public IActionResult AddBaseCost(BaseCostModel baseCost)
        {
            return Ok(_priceManager.AddBaseCost(baseCost));
        }

        [HttpPut("basecost")]
        public IActionResult EditBaseCost(BaseCostModel baseCost)
        {
            return Ok(_priceManager.EditBaseCost(baseCost));
        }

        [HttpPost("service")]
        public IActionResult AddCustomerService(CustomerServiceModel customerService)
        {
            return Ok(_priceManager.AddCustomerService(customerService));
        }

        [HttpPut("service")]
        public IActionResult EditCustomerService(CustomerServiceModel customerService)
        {
            return Ok(_priceManager.EditCustomerService(customerService));
        }
        [HttpPost("discount")]
        public IActionResult AddCustomerDiscount(CustomerDiscountModel customerDiscount)
        {
            return Ok(_priceManager.AddCustomerDiscount(customerDiscount));
        }

        [HttpPut("discount")]
        public IActionResult EditCustomerDiscount(CustomerDiscountModel customerDiscount)
        {
            return Ok(_priceManager.EditCustomerDiscount(customerDiscount));
        }
    }
}
