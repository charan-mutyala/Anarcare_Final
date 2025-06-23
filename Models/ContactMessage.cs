using System.ComponentModel.DataAnnotations;

namespace AnarcareWeb.Models
{
    public class ContactMessage
    {
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required, EmailAddress]
        public required string Email { get; set; }

        public required string Phone { get; set; }

        [Required]
        public required string Message { get; set; }
    }
}
