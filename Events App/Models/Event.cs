using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Events_App.Models
{
    public class Event
    {
        public int EventId { get; set; }

        [Required(ErrorMessage = "Titlul evenimentului este obligatoriu")]
        [StringLength(100, ErrorMessage = "Titlul nu poate avea mai mult de 100 de caractere")]
        [MinLength(2, ErrorMessage = "Titlul trebuie sa aiba mai mult de 2 caractere")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Descrierea subiectului este obligatorie")]
        public string Description { get; set; }

        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Locatia evenimentului este obligatorie")]
        public string Location { get; set; }
        public int? CategoryId { get; set; }

        public virtual Category? Category { get; set; }

        //public string? UserId { get; set; }
        //public virtual ApplicationUser? User { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem>? Categ { get; set; }
    }
}
