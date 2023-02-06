using ENB.Restaurant.Event.Bookings.Entities;

namespace ENB.Restaurant.Event.Bookings.MVC.Models
{
    public class DisplayMenuMeal
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public Menu? Menu { get; set; }
        public int? MealId { get; set; }
        public Meal? Meal { get; set; }
        public string? MenuName { get; set; }
        public string? MealName { get; set; }

        public DateTime Mealdate { get; set; }
        public string Other_Menu_Meal_detail { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
