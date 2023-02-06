﻿using ENB.Restaurant.Event.Bookings.EF;
using ENB.Restaurant.Event.Bookings.Entities;
using ENB.Restaurant.Event.Bookings.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENB.Restaurant.Event.Bookings.EF.Repositories
{

    /// <summary>
    /// A concrete repository to work with case in the system.
    /// </summary>
    public class AsyncCustomerRepository: AsyncRepository<Customer>, IAsyncCustomerRepository
    {
        /// <summary>
        /// Gets a list of all guests whose last name exactly matches the search string.
        /// </summary>
        /// <param name="name">The last name that the system should search for.</param>
        /// <returns>An IEnumerable of Person with the matching people.</returns>
        /// 

        private readonly RestaurantEventBookingContext _restaurantEventBookingContext;
        public AsyncCustomerRepository(RestaurantEventBookingContext restaurantEventBookingContext) : base(restaurantEventBookingContext)
        {
            _restaurantEventBookingContext = restaurantEventBookingContext;
        }
        public IEnumerable<Customer> FindByName(string lastname)
        {
            return _restaurantEventBookingContext.Set<Customer>().Where(x => x.LastName == lastname);
        }
    }
}
