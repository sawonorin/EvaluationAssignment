using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PricingService.Domain.Models
{
    public sealed class BaseCostModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Service Service { get; set; }
        public decimal Cost { get; set; }
        public bool Sunday { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public DateTime Start { get; set; }
    }
}
