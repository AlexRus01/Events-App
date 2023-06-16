using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace Events_App.Models
{
    public class Comment
    {
        public int CommentId { get; set; }

        [Required(ErrorMessage = "Continutul comentariului este obligatoriu")]
        public string Content { get; set; }

        public DateTime Date { get; set; }

        public int? EventId { get; set; }

        //public string? UserId { get; set; }
        //public virtual ApplicationUser? User { get; set; }
        public virtual Event? Event { get; set; }

        public virtual ApplicationUser User { get; set; } 
        public string? UserId { get; set; }

    }
}
