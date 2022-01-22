﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PricingService.Domain.Models
{
    public sealed class CustomerServiceModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid CustomerId { get; set; }
        public Service Service { get; set; }
        public DateTime Start { get; set; }
        public decimal? Price { get; set; }
    }
}
