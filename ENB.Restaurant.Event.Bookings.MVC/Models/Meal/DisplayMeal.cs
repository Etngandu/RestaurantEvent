using ENB.Restaurant.Event.Bookings.Entities.Collections;

namespace ENB.Restaurant.Event.Bookings.MVC.Models
{
    public class DisplayMeal
    {
        public int Id { get; set; }
        public DateTime Dateofmeal { get; set; }
        public string MealName { get; set; } = string.Empty;
        public string Other_MealDetail { get; set; } = string.Empty;
        public Menu_Meals? Menu_Meals { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
