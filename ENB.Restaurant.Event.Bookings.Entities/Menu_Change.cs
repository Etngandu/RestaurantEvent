using ENB.Restaurant.Event.Bookings.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENB.Restaurant.Event.Bookings.Entities
{
    public class Menu_Change : DomainEntity<int>, IDateTracking
    {

        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public int? BookingId { get; set; }
        public Booking? Booking { get; set; }

        public int Menu_BookedId { get; set; }
        public Menu_Booked? Menu_Booked { get; set; }

        public string Change_details { get; set; } = string.Empty;
        public DateTime DateCreated { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime DateModified { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
