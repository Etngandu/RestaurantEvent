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
    public class Menu : DomainEntity<int>, IDateTracking
    {
        public Menu()
        {
            Menus_Booked = new Menus_Booked();
            Menu_Meals = new Menu_Meals();
        }

        public string Menu_name { get; set; } = string.Empty;
        public string Menu_description { get; set; }= string.Empty;
        public string Otherdetails { get; set; } = string.Empty;
        public DateTime DateCreated { get; set ; }
        public DateTime DateModified { get ; set ; }
        public Menus_Booked Menus_Booked { get; set; }
        public  Menu_Meals Menu_Meals { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
