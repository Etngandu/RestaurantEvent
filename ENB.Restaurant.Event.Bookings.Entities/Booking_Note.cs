using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENB.Restaurant.Event.Bookings.Entities.Collections;
using ENB.Restaurant.Event.Bookings.Infrastructure;

namespace ENB.Restaurant.Event.Bookings.Entities
{
    public class Booking_Note : DomainEntity<int>, IDateTracking
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Customer class.
        /// </summary>
        public Booking_Note()
        {

          //  Prescription_Items = new Prescription_Items();            
        }

        #endregion
        #region "Public Properties"
        /// <summary>
        /// Gets or sets the navigation property of the Customer.
        /// </summary>
        /// 
        
        public Booking? Booking { get; set; }

        /// <summary>
        /// Gets or sets the Id of the Customer.
        /// </summary>
        /// 
        public int? BookingId { get; set; }

        /// <summary>
        /// Gets or sets the navigation property of the Customer.
        /// </summary>
        /// 

        public Customer? Customer { get; set; }

        /// <summary>
        /// Gets or sets the Id of the Customer.
        /// </summary>
        /// 
        public int CustomerId { get; set; }


        public string Details_of_notes { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date and time the object was last created.
        /// </summary>
        public DateTime DateCreated { get ; set ; }

        /// <summary>
        /// Gets or sets the date and time the object was last modified.
        /// </summary>
        /// 
        public DateTime DateModified { get ; set ; }

       
        #endregion

        #region "Methods"
        /// <summary>
        /// Validates this object. It validates dependencies between properties and also calls Validate on child collections;
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns>A IEnumerable of ValidationResult. The IEnumerable is empty when the object is in a valid state.</returns>
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Details_of_notes))
            {
                yield return new ValidationResult("Details_of_notes can't be None.", new[] { "Details_of_notes" });
            }
            

            
        }
        #endregion
    }
}
