using ENB.Restaurant.Event.Bookings.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ENB.Restaurant.Event.Bookings.EF
{
  public  class AsyncEFUnitOfWorkFactory :IAsyncUnitOfWorkFactory
    {
        private readonly RestaurantEventBookingContext _restaurantEventBookingContext;

      

        public AsyncEFUnitOfWorkFactory(RestaurantEventBookingContext restaurantEventBookingContext)
        {
            _restaurantEventBookingContext = restaurantEventBookingContext;

        }
        public AsyncEFUnitOfWorkFactory(bool forcenew, RestaurantEventBookingContext restaurantEventBookingContext)
        {
            _restaurantEventBookingContext = restaurantEventBookingContext;

        }
        /// <summary>
        /// Creates a new instance of an EFUnitOfWork.
        /// </summary>
        public async Task<IAsyncUnitOfWork> Create()
        {
            return await Create(false);
        }

        /// <summary>
        /// Creates a new instance of an EFUnitOfWork.
        /// </summary>
        /// <param name="forceNew">When true, clears out any existing data context from the storage container.</param>
        public async Task<IAsyncUnitOfWork> Create(bool forceNew)
        {
            var asyncEFUnitOfWork = await Task.FromResult(new AsyncEFUnitOfWork(forceNew, _restaurantEventBookingContext));


            return asyncEFUnitOfWork!;

        }


    }
}
