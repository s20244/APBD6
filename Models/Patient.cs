using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PharmacyAPI.Models
{
    public class Patient
    {
        public Patient()
        {
            Prescriptions = new List<Prescription>();
        }

        [Key]
        public int IdPatient { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        public DateTime Birthdate { get; set; }

        [JsonIgnore]
        public ICollection<Prescription> Prescriptions { get; set; }
    }
}
