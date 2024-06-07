using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PharmacyAPI.Models
{
    public class Doctor
    {
        public Doctor()
        {
            Prescriptions = new List<Prescription>();
        }

        [Key]
        public int IdDoctor { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [JsonIgnore]
        public ICollection<Prescription> Prescriptions { get; set; }
    }
}
