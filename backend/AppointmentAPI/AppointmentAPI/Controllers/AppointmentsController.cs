using AppointmentAPI.Data;
using AppointmentAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppointmentAPI.Controllers
{
    [ApiController]
    [Route("appointments")]
    public class AppointmentsController : ControllerBase
    {
        private readonly AppointmentContext _context;

        public AppointmentsController(AppointmentContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointments()
            => await _context.Appointments.ToListAsync();

        [HttpPost]
        public async Task<ActionResult<Appointment>> BookAppointment(Appointment appointment)
        {
            if (string.IsNullOrWhiteSpace(appointment.PatientName) ||
                string.IsNullOrWhiteSpace(appointment.DoctorName) ||
                appointment.StartTime == default ||
                appointment.EndTime <= appointment.StartTime)
                return BadRequest("Invalid appointment data.");

            var overlap = await _context.Appointments.AnyAsync(a =>
                a.DoctorName == appointment.DoctorName &&
                a.EndTime > appointment.StartTime &&
                a.StartTime < appointment.EndTime);

            if (overlap)
                return Conflict("Appointment overlaps with existing booking for this doctor.");

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAppointments), new { id = appointment.Id }, appointment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, Appointment updatedAppointment)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null) return NotFound();

            var overlap = await _context.Appointments.AnyAsync(a =>
                a.Id != id &&
                a.DoctorName == updatedAppointment.DoctorName &&
                a.EndTime > updatedAppointment.StartTime &&
                a.StartTime < updatedAppointment.EndTime);

            if (overlap)
                return Conflict("Appointment overlaps with existing booking for this doctor.");

            appointment.PatientName = updatedAppointment.PatientName;
            appointment.StartTime = updatedAppointment.StartTime;
            appointment.EndTime = updatedAppointment.EndTime;
            appointment.DoctorName = updatedAppointment.DoctorName;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelAppointment(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null) return NotFound();

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}
