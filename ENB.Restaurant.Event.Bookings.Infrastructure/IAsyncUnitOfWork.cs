﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENB.Restaurant.Event.Bookings.Infrastructure
{

    /// <summary>
    /// Represents a unit of work
    /// </summary>
    public interface IAsyncUnitOfWork : IAsyncDisposable
    {
        /// <summary>
        /// Commits the changes to the underlying data store. 
        /// </summary>
        /// <param name="">When true, all the previously retrieved objects should be cleared from the underlying model / cache.</param>
        Task Commit();


    }
}

