using System.ComponentModel.DataAnnotations;

namespace ENB.Restaurant.Event.Bookings.MVC.Models
{
    public class CreateAndEditMenu : IValidatableObject
    {
        public int Id { get; set; }
        public string Menu_name { get; set; } = string.Empty;
        public string Menu_description { get; set; } = string.Empty;
        public string Otherdetails { get; set; } = string.Empty;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Menu_name))
            {
                yield return new ValidationResult("Menu_name can't be empty", new[] { "Menu_name" });
            }
            if (string.IsNullOrEmpty(Menu_description))
            {
                yield return new ValidationResult("Menu_description can't be empty", new[] { "Menu_description" });
            }
        }
    }
}
