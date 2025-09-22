using AppointmentAPI.Data;
using AppointmentAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentAPI.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly AppointmentContext _context;

        public AppointmentService(AppointmentContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsAsync()
            => await _context.Appointments.ToListAsync();

        public async Task<Appointment?> GetAppointmentByIdAsync(int id)
            => await _context.Appointments.FindAsync(id);

        public async Task<(bool Success, string? ErrorMessage, Appointment? Appointment)> BookAppointmentAsync(Appointment appointment)
        {
            if (appointment.EndTime <= appointment.StartTime)
                return (false, "End time must be after start time.", null);

            var overlap = await _context.Appointments.AnyAsync(a =>
                a.DoctorName == appointment.DoctorName &&
                a.EndTime > appointment.StartTime &&
                a.StartTime < appointment.EndTime);

            if (overlap)
                return (false, "Appointment overlaps with existing booking for this doctor.", null);

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return (true, null, appointment);
        }

        public async Task<(bool Success, string? ErrorMessage)> UpdateAppointmentAsync(int id, Appointment updatedAppointment)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
                return (false, "Appointment not found.");

            if (updatedAppointment.EndTime <= updatedAppointment.StartTime)
                return (false, "End time must be after start time.");

            var overlap = await _context.Appointments.AnyAsync(a =>
                a.Id != id &&
                a.DoctorName == updatedAppointment.DoctorName &&
                a.EndTime > updatedAppointment.StartTime &&
                a.StartTime < updatedAppointment.EndTime);

            if (overlap)
                return (false, "Appointment overlaps with existing booking for this doctor.");

            appointment.PatientName = updatedAppointment.PatientName;
            appointment.StartTime = updatedAppointment.StartTime;
            appointment.EndTime = updatedAppointment.EndTime;
            appointment.DoctorName = updatedAppointment.DoctorName;

            await _context.SaveChangesAsync();
            return (true, null);
        }

        public async Task<(bool Success, string? ErrorMessage)> CancelAppointmentAsync(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
                return (false, "Appointment not found.");

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
            return (true, null);
        }
    }
}
