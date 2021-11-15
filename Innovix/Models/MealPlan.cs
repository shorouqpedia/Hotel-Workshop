using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Innovix.Models
{
    public class MealPlan
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal LowSeasonRate { get; set; }
        public decimal HighSeasonRate { get; set; }
    }
}
