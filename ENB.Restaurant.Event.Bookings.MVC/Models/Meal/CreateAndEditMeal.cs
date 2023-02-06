using ENB.Restaurant.Event.Bookings.Entities.Collections;
using System.ComponentModel.DataAnnotations;

namespace ENB.Restaurant.Event.Bookings.MVC.Models
{
    public class CreateAndEditMeal : IValidatableObject
    {
        public int Id { get; set; }
        public DateTime Dateofmeal { get; set; }
        public string MealName { get; set; } = string.Empty;
        public string Other_MealDetail { get; set; } = string.Empty;
        public Menu_Meals? Menu_Meals { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(MealName)){
                yield return new ValidationResult("MealName can't be Null", new[] { "MealName" });
            }
        }
    }
}
