using ENB.Restaurant.Event.Bookings.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENB.Restaurant.Event.Bookings.Entities.Collections
{
    
    //// <summary>
    ///// Represents a collection of PhoneNumber instances in the system.
    ///// </summary>
    public class ListBooking : CollectionBase<Booking>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneNumbers"/> class.
        /// </summary>
        public ListBooking() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneNumbers"/> class.
        /// </summary>
        /// <param name="initialList">Accepts an IList of PhoneNumber as the initial list.</param>
        public ListBooking(IList<Booking> initialList) : base(initialList) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneNumbers"/> class.
        /// </summary>
        /// <param name="initialList">Accepts a CollectionBase of PhoneNumber as the initial list.</param>
        public ListBooking(CollectionBase<Booking> initialList) : base(initialList) { }

        /// <summary>
        /// Adds a new instance of PhoneNumber to the collection.
        /// </summary>
        /// <param name="number">The e-phone number text.</param>
        /// <param name="contactType">The type of the phone number.</param>
        public void Add( Ref_payment_method ref_Payment_Method)
        {
            Add(new Booking() { Payment_Method = ref_Payment_Method });
        }

        /// <summary>
        /// Validates the current collection by validating each individual item in the collection.
        /// </summary>
        /// <returns>A IEnumerable of ValidationResult. The IEnumerable is empty when the object is in a valid state.</returns>
        public IEnumerable<ValidationResult> Validate()
        {
            var errors = new List<ValidationResult>();
            foreach (var number in this)
            {
                errors.AddRange(number.Validate());
            }
            return errors;
        }
    }
}
