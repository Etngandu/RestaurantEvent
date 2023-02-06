using ENB.Restaurant.Event.Bookings.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENB.Restaurant.Event.Bookings.Entities
{
    public class Menu_Meal     {

        public int MenuId { get; set; }
        public Menu? Menu { get; set; }
        public int? MealId { get; set; }
        public Meal? Meal { get; set; }
        
    }
}
