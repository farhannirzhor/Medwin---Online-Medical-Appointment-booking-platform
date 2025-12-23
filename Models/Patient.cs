using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace MedicalAppointmentSystem.Models
{
    public class Patient
    {
        public int Id { get; set; }   // Primary Key

        [Required]
        public string FullName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }

        public string Password { get; set; } // temporary (we’ll improve later)
        public ICollection<Appointment> Appointments { get; set; }

    }
}
