using MedicalAppointmentSystem.Data;
using MedicalAppointmentSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedicalAppointmentSystem.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly EmailService _emailService;

        public AdminController(ApplicationDbContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;   // ✅ ADD THIS
        }


        // Admin Dashboard
        public IActionResult Dashboard()
        {
            var appointments = _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .OrderByDescending(a => a.AppointmentDate)
                .ToList();

            return View(appointments);
        }

        // Approve appointment
        public IActionResult Approve(int id)
        {
            var appointment = _context.Appointments
                .FirstOrDefault(a => a.Id == id);

            if (appointment != null)
            {
                appointment.Status = "Approved";
                _context.SaveChanges();

                // ✅ SEND EMAIL TO USER-PROVIDED EMAIL
                _emailService.SendEmail(
                    appointment.PatientEmail,
                    "Appointment Approved",
                    $"Dear {appointment.PatientName},\n\n" +
                    "Your appointment has been APPROVED.\n\n" +
                    "Please check your patient dashboard for details.\n\n" +
                    "Regards,\nMedWin"
                );
            }

            return RedirectToAction("Dashboard");
        }

        [HttpPost]
        public IActionResult UploadPrescription(int appointmentId, IFormFile prescriptionFile)
        {
            if (prescriptionFile != null && prescriptionFile.Length > 0)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(prescriptionFile.FileName);
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(),
                                              "wwwroot/prescriptions");

                var filePath = Path.Combine(folderPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    prescriptionFile.CopyTo(stream);
                }

                var appointment = _context.Appointments.Find(appointmentId);
                if (appointment != null)
                {
                    appointment.PrescriptionFilePath = "/prescriptions/" + fileName;
                    _context.SaveChanges();
                }
            }

            return RedirectToAction("Dashboard");
        }


        // Reject appointment
        public IActionResult Reject(int id)
        {
            var appointment = _context.Appointments
                .Include(a => a.Doctor)
                .FirstOrDefault(a => a.Id == id);

            if (appointment != null)
            {
                appointment.Status = "Rejected";
                _context.SaveChanges();

                _emailService.SendEmail(
                    appointment.PatientEmail,
                    "Appointment Rejected",
                    $"Dear {appointment.PatientName},\n\n" +
                    "Unfortunately, your appointment request has been REJECTED.\n\n" +
                    "Please try another time.\nMedWin"
                );
            }

            return RedirectToAction("Dashboard");
        }

    }
}
