using AppointmentAPI.Models;

namespace AppointmentAPI.Services
{
    public interface IAppointmentService
    {
        Task<IEnumerable<Appointment>> GetAppointmentsAsync();
        Task<Appointment?> GetAppointmentByIdAsync(int id);
        Task<(bool Success, string? ErrorMessage, Appointment? Appointment)> BookAppointmentAsync(Appointment appointment);
        Task<(bool Success, string? ErrorMessage)> UpdateAppointmentAsync(int id, Appointment updatedAppointment);
        Task<(bool Success, string? ErrorMessage)> CancelAppointmentAsync(int id);
    }
}
