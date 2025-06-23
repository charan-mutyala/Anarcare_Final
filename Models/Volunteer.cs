using System.ComponentModel.DataAnnotations;

namespace AnarcareWeb.Models
{
    public class Volunteer
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        [Range(1, 120)]
        public int Age { get; set; }

        [Required]
        public string Motivation { get; set; }
    }
}
