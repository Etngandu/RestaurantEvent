using ENB.Restaurant.Event.Bookings.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ENB.Restaurant.Event.Bookings.MVC.Models
{
    public class CreateAndEditMenuBooked
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public int MenuId { get; set; }
        public Menu? Menu { get; set; }
        public int? BookingId { get; set; }
        public Booking? Booking { get; set; }
        public List<SelectListItem>? ListMenu { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
