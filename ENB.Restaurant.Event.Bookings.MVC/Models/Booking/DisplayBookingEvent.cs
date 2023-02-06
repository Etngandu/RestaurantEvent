using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ENB.Restaurant.Event.Bookings.Entities;
using ENB.Restaurant.Event.Bookings.Entities.Collections;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ENB.Restaurant.Event.Bookings.MVC.Models
{
    public class DisplayBookingEvent
    {
        public int Id { get; set; }

        public Customer? Customer { get; set; }


        public int CustomerId { get; set; }

        public Staff? Staff { get; set; }

        public int? StaffId { get; set; }

        public EventStatus EventStatus { get; set; }

        public Ref_payment_method Payment_Method { get; set; }

        [Display(Name = "Date of event")]
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public string? Color { get; set; }
        public Boolean AllDay { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? NameStaffmenber { get; set; }
        public string? Namecustomer { get; set; }
        public string? BookingNumber { get; set; }
        public string Other_details { get; set; } = string.Empty;

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }
}
