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
    public class BookingNotes : CollectionBase<Booking_Note>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneNumbers"/> class.
        /// </summary>
        public BookingNotes() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneNumbers"/> class.
        /// </summary>
        /// <param name="initialList">Accepts an IList of PhoneNumber as the initial list.</param>
        public BookingNotes(IList<Booking_Note> initialList) : base(initialList) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PhoneNumbers"/> class.
        /// </summary>
        /// <param name="initialList">Accepts a CollectionBase of PhoneNumber as the initial list.</param>
        public BookingNotes(CollectionBase<Booking_Note> initialList) : base(initialList) { }

        

        /// <summary>
        /// Validates the current collection by validating each individual item in the collection.
        /// </summary>
        /// <returns>A IEnumerable of ValidationResult. The IEnumerable is empty when the object is in a valid state.</returns>
        public IEnumerable<ValidationResult> Validate()
        {
            var errors = new List<ValidationResult>();
            foreach (var booking_note in this)
            {
                errors.AddRange(booking_note.Validate());
            }
            return errors;
        }
    }
}
