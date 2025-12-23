using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace MedicalAppointmentSystem.Models
{
    public class Doctor
    {
        public int Id { get; set; }   // Primary Key

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Department { get; set; }

        public string AvailableDays { get; set; }   // e.g. Mon–Thu
        public string AvailableTime { get; set; }   // e.g. 10 AM – 2 PM
        public ICollection<Appointment> Appointments { get; set; }

    }
}
