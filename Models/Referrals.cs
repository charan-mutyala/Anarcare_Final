using System.ComponentModel.DataAnnotations;

namespace AnarcareWeb.Models
{
    public class Referral
    {
        public int Id { get; set; }

        [Required]
        public string ReferrerName { get; set; }

        [Required]
        public string ReferrerPhone { get; set; }

        [Required]
        public string PatientName { get; set; }

        [Required]
        public string PatientPhone { get; set; }

        public string? Comments { get; set; }
    }
}
