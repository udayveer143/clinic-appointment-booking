using System.ComponentModel.DataAnnotations;
using AppointmentAPI.Validation;

namespace AppointmentAPI.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Patient name is required.")]
        [MaxLength(100, ErrorMessage = "Patient name cannot exceed 100 characters.")]
        public string PatientName { get; set; }

        [Required(ErrorMessage = "Doctor name is required.")]
        [MaxLength(100, ErrorMessage = "Doctor name cannot exceed 100 characters.")]
        public string DoctorName { get; set; }

        [Required(ErrorMessage = "Start time is required.")]
        [FutureOrNowDate(ErrorMessage = "Start time must be now or in the future.")]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "End time is required.")]
        [EndAfterStartDate("StartTime", ErrorMessage = "End time must be after the start time.")]
        public DateTime EndTime { get; set; }
    }
}
