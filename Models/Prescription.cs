using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PharmacyAPI.Models
{
    public class Prescription
    {
        public Prescription()
        {
            PrescriptionMedicaments = new List<PrescriptionMedicament>();
        }

        [Key]
        public int IdPrescription { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [ForeignKey("Patient")]
        public int IdPatient { get; set; }

        [JsonIgnore]
        public Patient Patient { get; set; }

        [ForeignKey("Doctor")]
        public int IdDoctor { get; set; }

        [JsonIgnore]
        public Doctor Doctor { get; set; }

        [JsonIgnore]
        public ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    }
}
