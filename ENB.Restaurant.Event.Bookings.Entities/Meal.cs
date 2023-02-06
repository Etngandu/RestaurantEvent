using ENB.Restaurant.Event.Bookings.Entities.Collections;
using ENB.Restaurant.Event.Bookings.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENB.Restaurant.Event.Bookings.Entities
{
    public class Meal : DomainEntity<int>, IDateTracking
    {
        public Meal()
        {
            Menu_Meals = new();
        }
        public DateTime Dateofmeal { get; set; }
        public string MealName { get; set; } = string.Empty;
        public string Other_MealDetail { get; set; } = string.Empty;
        public Menu_Meals Menu_Meals { get; set; }
        public DateTime DateCreated { get ; set ; }
        public DateTime DateModified { get ; set ; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
