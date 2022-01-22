using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PricingService.DataAccess.Entities
{
    public class BaseCost : BaseEntity
    {
        public int Service { get; set; }
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
