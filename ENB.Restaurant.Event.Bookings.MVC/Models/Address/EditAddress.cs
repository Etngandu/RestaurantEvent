﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ENB.Restaurant.Event.Bookings.Entities;
using ENB.Restaurant.Event.Bookings.MVC.Models;

namespace ENB.Restaurant.Event.Bookings.MVC.Models
{
    public class EditAddress : IValidatableObject
    {
        public int CustomerId { get; set; }
        public int StaffId { get; set; }       
        public string? Number_street { get; set; }
        public string? City { get; set; }
        public string? Zipcode { get; set; }
        public string? State_province_county { get; set; }
        public string? Country { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new Address(Number_street!, City!, Zipcode!, State_province_county!, Country!).Validate();
        }
    }
}