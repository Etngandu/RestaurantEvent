using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ENB.Restaurant.Event.Bookings.Infrastructure;
using ENB.Restaurant.Event.Bookings.Entities;

namespace ENB.Restaurant.Event.Bookings.EF.ConfigurationEntity
{
   public class BookingConfiguration:IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.Property(x => x.Other_details).IsRequired().HasMaxLength(350);
            builder.Property(x => x.Color).IsRequired().HasMaxLength(150);

        }
    }
}
