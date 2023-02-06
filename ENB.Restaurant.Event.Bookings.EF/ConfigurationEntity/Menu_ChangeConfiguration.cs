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
   public class Menu_ChangeConfiguration:IEntityTypeConfiguration<Menu_Change>
    {
        public void Configure(EntityTypeBuilder<Menu_Change> builder)
        {
            builder.Property(x => x.Change_details).IsRequired().HasMaxLength(350);            

        }
    }
}
