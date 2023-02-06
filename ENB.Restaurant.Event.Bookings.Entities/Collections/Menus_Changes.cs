using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENB.Restaurant.Event.Bookings.Entities.Collections
{
    
    public class Menus_Changes : CollectionBase<Menu_Change>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Operators"/> class.
        /// </summary>
        public Menus_Changes() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Operators"/> class.
        /// </summary>
        /// <param name="initialList">Accepts an IList of Person as the initial list.</param>
        public Menus_Changes(IList<Menu_Change> initialList) : base(initialList) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Operators"/> class.
        /// </summary>
        /// <param name="initialList">Accepts a CollectionBase of Person as the initial list.</param>
        public Menus_Changes(CollectionBase<Menu_Change> initialList) : base(initialList) { }

        /// <summary>
        /// Validates the current collection by validating each individual item in the collection.
        /// </summary>
        /// <returns>A IEnumerable of ValidationResult. The IEnumerable is empty when the object is in a valid state.</returns>
        public IEnumerable<ValidationResult> Validate()
        {
            var errors = new List<ValidationResult>();
            foreach (var menu_meal in this)
            {
                errors.AddRange(menu_meal.Validate());
            }
            return errors;
        }
    }
}
