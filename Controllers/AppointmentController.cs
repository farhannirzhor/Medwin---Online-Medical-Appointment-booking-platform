using MedicalAppointmentSystem.Data;
using MedicalAppointmentSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MedicalAppointmentSystem.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppointmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Book(Appointment appointment)
        {
            // TEMP logged-in patient
            appointment.PatientId = 1;

            // REQUIRED DB FIELDS
            appointment.Status = "Pending";

            // ✅ EMAIL VALIDATION (THIS WAS MISSING)
            if (string.IsNullOrEmpty(appointment.PatientEmail))
            {
                TempData["Error"] = "Email is required.";
                return RedirectToAction("Index", "Home");
            }

            // SAFETY DEFAULTS
            if (string.IsNullOrEmpty(appointment.PatientName))
                appointment.PatientName = "Test Patient";

            if (appointment.DoctorId == 0)
                appointment.DoctorId = 1;

            if (appointment.AppointmentDate == DateTime.MinValue)
                appointment.AppointmentDate = DateTime.Today;

            if (string.IsNullOrEmpty(appointment.AppointmentTime))
                appointment.AppointmentTime = "10:00 AM";

            if (string.IsNullOrEmpty(appointment.ProblemDescription))
                appointment.ProblemDescription = "Not provided";

            // Prescription not uploaded yet
            appointment.PrescriptionFilePath = "";

            // SAVE
            _context.Appointments.Add(appointment);
            _context.SaveChanges();

            TempData["Success"] = "Appointment booked successfully!";
            return RedirectToAction("Dashboard", "Patient");
        }



    }
}
