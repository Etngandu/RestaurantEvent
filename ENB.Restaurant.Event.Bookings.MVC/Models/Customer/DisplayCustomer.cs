using ENB.Restaurant.Event.Bookings.Entities.Collections;
using ENB.Restaurant.Event.Bookings.Entities;

namespace ENB.Restaurant.Event.Bookings.MVC.Models
{
    public class DisplayCustomer
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = String.Empty;

        public string LastName { get; set; } = String.Empty;

        public string FullName { get; set; } = string.Empty;

        public string EmailAddress { get; set; } = String.Empty;

        public Gender Gender { get; set; }

        public string PhoneNumber { get; set; } = String.Empty;

        public DateTime DateOfBirth { get; set; }


        public string Credit_card_Number { get; set; } = String.Empty;


        public string Card_expiry_date { get; set; } = String.Empty;


        public string Other_details { get; set; } = String.Empty;

        public ListBooking? ListBooking { get; set; }

        public Address? AddressCustomer { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}

