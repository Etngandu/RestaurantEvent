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
   public class Booking_NoteConfiguration:IEntityTypeConfiguration<Booking_Note>
    {
        public void Configure(EntityTypeBuilder<Booking_Note> builder)
        {
            builder.Property(x => x.Details_of_notes).IsRequired().HasMaxLength(350);            

        }
    }
}
