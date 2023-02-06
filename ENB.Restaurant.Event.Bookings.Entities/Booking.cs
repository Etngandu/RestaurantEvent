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
    public class Booking : DomainEntity<int>, IDateTracking
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Customer class.
        /// </summary>
        public Booking()
        {

            BookingNotes = new BookingNotes();
            Menus_Booked = new Menus_Booked();
        }

        #endregion
        #region "Public Properties"
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

        /// <summary>
        /// Gets or sets the navigation property of the physician.
        /// </summary>
        ///        
        public Staff? Staff { get; set; }

        /// <summary>
        /// Gets or sets the Id of the staff.
        /// </summary>
        /// 
        public int? StaffId { get; set; }

        /// <summary>
        /// Gets or sets the Booking status.
        /// </summary>
        /// 
        public EventStatus EventStatus { get; set; }

        /// <summary>
        /// Gets or sets the Payment_Method.
        /// </summary>
        /// 
        public Ref_payment_method Payment_Method { get; set; }
        

        /// <summary>
        /// Gets or sets the Presciption_filled_date.
        /// </summary>
        /// 
        [Display(Name = "Date of event")]
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public string? Color { get; set; }
        public Boolean AllDay { get; set; }

        /// <summary>
        /// Gets or sets the BookingNotes of this customer.
        /// </summary>

        public BookingNotes BookingNotes { get; set; }

        /// <summary>
        /// Gets or sets the Menus_booked of this customer.
        /// </summary>

        public Menus_Booked Menus_Booked { get; set; }

        public string Other_details { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date and time the object was last created.
        /// </summary>
        public DateTime DateCreated { get ; set ; }

        /// <summary>
        /// Gets or sets the date and time the object was last modified.
        /// </summary>
        /// 
        public DateTime DateModified { get ; set ; }

        /// <summary>
        /// Gets the full name of this person.
        /// </summary>
        public string BookingNumber
        {
            get
            {
                string temp = DateCreated.ToLongTimeString() ?? string.Empty;
                if (!string.IsNullOrEmpty(StaffId.ToString()) &&
                    !string.IsNullOrEmpty(CustomerId.ToString()))
                {

                    if (temp.Length > 0)
                    {
                        temp += "-";
                    }
                    temp += StaffId.ToString() + "/" +CustomerId.ToString();
                }
                return temp.Replace(":","");
            }
        }
        #endregion

        #region "Methods"
        /// <summary>
        /// Validates this object. It validates dependencies between properties and also calls Validate on child collections;
        /// </summary>
        /// <param name="validationContext"></param>
        /// <returns>A IEnumerable of ValidationResult. The IEnumerable is empty when the object is in a valid state.</returns>
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EventStatus == EventStatus.None)
            {
                yield return new ValidationResult("EventStatus can't be None.", new[] { "EventStatus" });
            }
            if (Payment_Method == Ref_payment_method.None)
            {
                yield return new ValidationResult("Payment_Method can't be None.", new[] { "Payment_Method" });
            }

            
        }
        #endregion
    }
}
