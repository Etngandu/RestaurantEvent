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
    public class CreateAndEditBookingEvent : IValidatableObject
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


        public List<SelectListItem>? ListCustomers { get; set; }

        public List<SelectListItem>? ListStaff { get; set; }

        public string Other_details { get; set; } = string.Empty;      


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            
            if (EventStatus==EventStatus.None)
            {
                yield return new ValidationResult("EventStatus can't be None.", new[] { "EventStatus" });
            }
            if (Payment_Method == Ref_payment_method.None)
            {
                yield return new ValidationResult("Payment_Method can't be None.", new[] { "Payment_Method" });
            }
        }
    }
}
