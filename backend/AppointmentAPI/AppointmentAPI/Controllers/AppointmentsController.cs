using AppointmentAPI.Models;
using AppointmentAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentAPI.Controllers
{
    [ApiController]
    [Route("appointments")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointments()
            => Ok(await _appointmentService.GetAppointmentsAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> GetAppointment(int id)
        {
            var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
            if (appointment == null) return NotFound();
            return Ok(appointment);
        }

        [HttpPost]
        public async Task<ActionResult<Appointment>> BookAppointment(Appointment appointment)
        {
            var result = await _appointmentService.BookAppointmentAsync(appointment);
            if (!result.Success)
                return BadRequest(result.ErrorMessage);

            return CreatedAtAction(nameof(GetAppointment), new { id = result.Appointment!.Id }, result.Appointment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, Appointment updatedAppointment)
        {
            var result = await _appointmentService.UpdateAppointmentAsync(id, updatedAppointment);
            if (!result.Success)
            {
                if (result.ErrorMessage == "Appointment not found.") return NotFound();
                return BadRequest(result.ErrorMessage);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> CancelAppointment(int id)
        {
            var result = await _appointmentService.CancelAppointmentAsync(id);
            if (!result.Success)
            {
                if (result.ErrorMessage == "Appointment not found.") return NotFound();
                return BadRequest(result.ErrorMessage);
            }
            return NoContent();
        }
    }
}
