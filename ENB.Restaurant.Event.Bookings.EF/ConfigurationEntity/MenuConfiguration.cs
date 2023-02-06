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
   public class MenuConfiguration:IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.Property(x => x.Menu_name).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Menu_description).IsRequired().HasMaxLength(350);
            builder.Property(x => x.Otherdetails).IsRequired().HasMaxLength(450);  

        }
    }
}
