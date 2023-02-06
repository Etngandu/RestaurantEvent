using ENB.Restaurant.Event.Bookings.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENB.Restaurant.Event.Bookings.Entities
{
    public class Menu_Booked : DomainEntity<int>, IDateTracking
    {

        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public int MenuId { get; set; }
        public Menu? Menu { get; set; }
        public int? BookingId { get; set; }
        public Booking? Booking { get; set; }
        public DateTime DateCreated { get ; set ; }
        public DateTime DateModified { get ; set ; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
