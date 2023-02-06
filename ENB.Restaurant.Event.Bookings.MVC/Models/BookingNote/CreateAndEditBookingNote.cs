using ENB.Restaurant.Event.Bookings.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ENB.Restaurant.Event.Bookings.MVC.Models
{
    public class CreateAndEditBookingNote:IValidatableObject
    {
        public int Id { get; set; }
        public Customer? Customer { get; set; }
        public int CustomerId { get; set; }
        public Booking? Booking { get; set; }
        public int BookingId { get; set; }
        public string Details_of_notes { get; set; } = string.Empty;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Details_of_notes)) 
            {
                yield return new ValidationResult("Details of notes can't be none", new[] { "Details_of_notes" });
            }
        }
    }
}
