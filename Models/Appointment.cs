using MedicalAppointmentSystem.Models;
using System.ComponentModel.DataAnnotations;

public class Appointment
{
    public int Id { get; set; }

    public DateTime AppointmentDate { get; set; }
    public string AppointmentTime { get; set; }

    public string ProblemDescription { get; set; }
    public string Status { get; set; } = "Pending";
    public string PrescriptionFilePath { get; set; }

    // 🔥 ADD THIS
    public string PatientName { get; set; }
    
    [Required]
    [EmailAddress]
    public string PatientEmail { get; set; }

    public int PatientId { get; set; }
    public Patient Patient { get; set; }

    public int DoctorId { get; set; }
    public Doctor Doctor { get; set; }
}
