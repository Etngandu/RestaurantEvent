﻿using ENB.Restaurant.Event.Bookings.Entities;
using ENB.Restaurant.Event.Bookings.Entities.Collections;
using ENB.Restaurant.Event.Bookings.MVC.Models;
using System.ComponentModel.DataAnnotations;

namespace ENB.Restaurant.Event.Bookings.MVC.Models
{
    public class CreateAndEditStaff: IValidatableObject
    {
        #region "Public Properties"

        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;


        public string LastName { get; set; } = string.Empty;

        public string EmailAddress { get; set; } = string.Empty;

        public Gender Gender { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;

        public DateTime DateOfBirth
        {
            get;
            set;
        }

        

        public string Other_details { get; set; } = string.Empty;   
       

        public ListBooking? ListBooking { get; set; }
        #endregion

        #region validation
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Gender==Gender.None)
            {
                yield return new ValidationResult("Gender can't be None.", new[] { "Gender" });
            }
            if (String.IsNullOrEmpty(PhoneNumber))
            {
                yield return new ValidationResult("PhoneNumber can't be None.", new[] { "PhoneNumber" });
            }

            if (String.IsNullOrEmpty(EmailAddress))
            {
                yield return new ValidationResult("EmailAddress can't be None.", new[] { "EmailAddress" });
            }
        }
        #endregion
    }
}
