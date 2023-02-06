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
   public class Menu_MealConfiguration:IEntityTypeConfiguration<Menu_Meal>
    {
        public void Configure(EntityTypeBuilder<Menu_Meal> builder)
        {
            builder.HasKey(x => new { x.MealId, x.MenuId });
        }
    }
}
