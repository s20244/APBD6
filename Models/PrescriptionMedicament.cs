using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PharmacyAPI.Models
{
    public class PrescriptionMedicament
    {
        [Key, Column(Order = 0)]
        public int IdMedicament { get; set; }

        [JsonIgnore]
        public Medicament Medicament { get; set; }

        [Key, Column(Order = 1)]
        public int IdPrescription { get; set; }

        [JsonIgnore]
        public Prescription Prescription { get; set; }

        [Required]
        public int Dose { get; set; }

        [Required]
        public string Details { get; set; }
    }
}
