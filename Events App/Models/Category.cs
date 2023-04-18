using System.ComponentModel.DataAnnotations;

namespace Events_App.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Numele categoriei este obligatoriu")]
        public string CategoryName { get; set; }
        public virtual ICollection<Event>? Events { get; set; }
    }
}
