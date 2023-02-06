using ENB.Restaurant.Event.Bookings.Entities;

namespace ENB.Restaurant.Event.Bookings.MVC.Models
{
    public class DisplayBookingNote
    {
        public int Id { get; set; }
        public Customer? Customer { get; set; }
        public int CustomerId { get; set; }
        public Booking? Booking { get; set; }
        public int BookingId { get; set; }
        public string? BookingNumber { get; set; }
        public string Details_of_notes { get; set; } = string.Empty;
        public string NameCustomer { get; set; } = string.Empty;
        public string NameStaffmenber { get; set; } = string.Empty;       
        public DateTime DateCreated { get; set; }        
        public DateTime DateModified { get; set; }
    }
}
