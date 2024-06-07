using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PharmacyAPI.Models
{
    public class Medicament
    {
        public Medicament()
        {
            PrescriptionMedicaments = new List<PrescriptionMedicament>();
        }

        [Key]
        public int IdMedicament { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }

        [Required]
        [MaxLength(100)]
        public string Type { get; set; }

        [JsonIgnore]
        public ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    }
}
