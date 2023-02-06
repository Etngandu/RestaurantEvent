using ENB.Restaurant.Event.Bookings.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ENB.Restaurant.Event.Bookings.MVC.Models
{
    public class CreateAndEditMenuMeal: IValidatableObject
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public Menu? Menu { get; set; }
        public int? MealId { get; set; }
        public Meal? Meal { get; set; }

        public List<SelectListItem>? ListMeal { get; set; }

        public List<SelectListItem>? ListMenu { get; set; }
        public string Other_Menu_Meal_detail { get; set; } = string.Empty;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(MenuId == 0) 
            {
                yield return new ValidationResult("Menu_Meal can't be Null", new[] { "MenuId" });
            }

            if (MealId == 0)
            {
                yield return new ValidationResult("Menu_Meal can't be Null", new[] { "MealId" });
            }
        }
    }
}
