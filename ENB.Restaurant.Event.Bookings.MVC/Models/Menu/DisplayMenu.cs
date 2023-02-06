namespace ENB.Restaurant.Event.Bookings.MVC.Models
{
    public class DisplayMenu
    {
        public int Id { get; set; }
        public string Menu_name { get; set; } = string.Empty;
        public string Menu_description { get; set; } = string.Empty;
        public string Otherdetails { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
