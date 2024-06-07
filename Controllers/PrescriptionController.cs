using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmacyAPI.Context;
using PharmacyAPI.Models;
using PharmacyAPI.RequestModels;

namespace PharmacyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PrescriptionController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePrescription(PrescriptionDto prescriptionDto)
        {
            if (prescriptionDto.DueDate < prescriptionDto.Date)
            {
                return BadRequest("DueDate cannot be earlier than Date");
            }

            if (prescriptionDto.Medicaments.Count > 10)
            {
                return BadRequest("A prescription can contain a maximum of 10 medicaments");
            }

            var doctor = await _context.Doctors.FindAsync(prescriptionDto.IdDoctor);
            if (doctor == null)
            {
                return BadRequest("Doctor not found");
            }

            var patient = await _context.Patients
                .Include(p => p.Prescriptions)
                .ThenInclude(pr => pr.PrescriptionMedicaments)
                .ThenInclude(pm => pm.Medicament)
                .Include(p => p.Prescriptions)
                .ThenInclude(pr => pr.Doctor)
                .FirstOrDefaultAsync(p => p.IdPatient == prescriptionDto.IdPatient);

            if (patient == null)
            {
                patient = new Patient
                {
                    FirstName = prescriptionDto.Patient.FirstName,
                    LastName = prescriptionDto.Patient.LastName,
                    Birthdate = prescriptionDto.Patient.Birthdate,
                    Prescriptions = new List<Prescription>()
                };
                _context.Patients.Add(patient);
                await _context.SaveChangesAsync();
            }

            var prescription = new Prescription
            {
                Date = prescriptionDto.Date,
                DueDate = prescriptionDto.DueDate,
                Doctor = doctor,
                Patient = patient,
                PrescriptionMedicaments = new List<PrescriptionMedicament>()
            };

            foreach (var medicamentDto in prescriptionDto.Medicaments)
            {
                var medicament = await _context.Medicaments.FindAsync(medicamentDto.IdMedicament);
                if (medicament == null)
                {
                    return BadRequest($"Medicament with ID {medicamentDto.IdMedicament} not found");
                }

                var prescriptionMedicament = new PrescriptionMedicament
                {
                    Medicament = medicament,
                    Dose = medicamentDto.Dose,
                    Details = medicamentDto.Details
                };

                prescription.PrescriptionMedicaments.Add(prescriptionMedicament);
            }

            patient.Prescriptions.Add(prescription);
            _context.Prescriptions.Add(prescription);
            await _context.SaveChangesAsync();

            return Created("", prescriptionDto);
        }
    }
}
