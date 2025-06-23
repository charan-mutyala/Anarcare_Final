using System.ComponentModel.DataAnnotations;

namespace AnarcareWeb.Models
{
    public class Employment
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Position { get; set; }

        public string? Comments { get; set; }

        public string? ResumeFileName { get; set; }  // stores the uploaded filename
    }
}
