using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using reservations.Interfaces;
using reservations.Models;

namespace reservations.Controllers
{
    [Route("[controller]")]
    public class AppointmentController : ControllerBase
    {
        public AppointmentController(IAppointmentRepository appointmentRepository)
        {
            Appointments = appointmentRepository;
        }

        public IAppointmentRepository Appointments { get; set; }        

        [HttpGet("api/GetAppointments")]
        public IEnumerable<Appointment> GetAll()
        {
            return Appointments.GetAll();
        }
        [HttpGet("api/GetBySpecialty")]
        public IEnumerable<Appointment> GetBySpecialty(string specialty)
        {
            return Appointments.GetBySpecialty(specialty);
        }
        [HttpGet("api/GetById/{id}")]
        public IActionResult GetById(long id)
        {
            var apt = Appointments.Find(id);
            if (apt == null)
                return NotFound();
            
            return new ObjectResult(apt);
        }
        [HttpGet("api/GetByProviderId/{providerId}/{dateTime}")]
        public IActionResult GetByProviderId(long providerId, DateTime dateTime)
        {
            var apt = Appointments.FindByProvider(providerId, dateTime);
            if (apt == null)
                return NotFound();
            
            return new ObjectResult(apt);
        }
        [HttpGet("api/GetByClientId/{clientid}/{dateTime}")]
        public IActionResult GetByClientId(long clientid, DateTime dateTime)
        {
            var apt = Appointments.FindByClient(clientid, dateTime);
            if (apt == null)
                return NotFound();
            
            return new ObjectResult(apt);
 
        }
        [HttpPost]
        public IActionResult Create([FromBody] Appointment appointment)
        {
            if (appointment == null)
                return BadRequest();
            Appointments.AddAppointment(appointment);
            return CreatedAtAction("GetById", new { id = appointment.Id }, appointment);
        }
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Appointment appointment)
        {
            if (appointment == null || appointment.Id != id)
                return BadRequest();
            var apt = Appointments.Find(id);
            if (apt == null)
                return NotFound();
            apt.AppointmentStart = appointment.AppointmentStart;
            apt.ClientId = appointment.ClientId;
            apt.CreatedDate = appointment.CreatedDate;
            apt.IsConfirmed = appointment.IsConfirmed;
            apt.ProviderId = appointment.ProviderId;
            apt.UpdatedDate = DateTime.Now;

            Appointments.Update(apt);
            return new NoContentResult();
        }
        [HttpPatch("api/Confirm/{id}")]
        public IActionResult ConfirmAppointment(long id)
        {
            var apt = Appointments.Find(id);
            if (apt == null)
                return NotFound();
            apt.IsConfirmed = true;
            apt.UpdatedDate = DateTime.Now;

            Appointments.Update(apt);
            return new NoContentResult();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var apt = Appointments.Find(id);
            if (apt == null)
                return NotFound();
            Appointments.RemoveAppointment(id);
            return new NoContentResult();
        }
    }
}