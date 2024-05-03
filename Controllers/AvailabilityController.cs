using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using reservations.Interfaces;
using reservations.Models;

namespace reservations.Controllers
{
    [Route("[controller]")]
    public class AvailabilityController : ControllerBase
    {
        public AvailabilityController(IAvailabilityRepository availabilityRepository)
        {
            Availabilities = availabilityRepository;
        }

        public IAvailabilityRepository Availabilities { get; set; }
        
        [HttpGet("api/GetAvsssssssailableTimess/{date}")]
        public IActionResult GetAvailableTimes(DateOnly date) 
        {
            var times = Availabilities.GetAvailableTimes(date);
            if (times == null)
                return NotFound();
            return new ObjectResult(times);
            
        }
        [HttpGet("api/GetAllAvailabilities")]
        public IEnumerable<Availability> GetAvailabilities()
        {
            return Availabilities.GetAvailabilities();
        }
        [HttpGet("api/GetAvailability/{Id}")]
        public IActionResult Find(long Id)
        {
            var a = Availabilities.Find(Id);
            if (a == null)
                return NotFound();
            return new ObjectResult(a);
        }
        [HttpGet("api/GetAvailabilitiesByProviderDate/{provierid}/{date}")]
        public IActionResult FindAvailability(long providerId, DateOnly date)
        {

            var a = Availabilities.FindAvailability(providerId, date);
            if (a == null)
                return NotFound();
            return new ObjectResult(a);
        }
        [HttpDelete("{id}")]
        public IActionResult RemoveAvailability(long Id)
        {
            var a = Availabilities.Find(Id);
            if (a == null)
                return NotFound();
            
            Availabilities.RemoveAvailability(Id);
            return new NoContentResult();
        }        
        [HttpDelete("api/DeleteByProviderDate{providerId}/{date}")]
        public IActionResult RemoveAvailability(long providerId, DateOnly date)
        {
            var a = Availabilities.FindAvailability(providerId,date);
            if (a == null)
                return NotFound();
            Availabilities.RemoveAvailability(providerId, date);
            return new NoContentResult();
        }
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Availability availability)
        {
            if (availability == null || availability.Id != id)
                return BadRequest();
            var a = Availabilities.Find(id);
            if (a == null)
                return NotFound(); 

            a.AvailableDate = availability.AvailableDate;
            a.Start = availability.Start;
            a.End = availability.End;
            a.ProviderId = availability.ProviderId;
            a.UpdatedDate = DateTime.Now;

            Availabilities.Update(a);
            return new NoContentResult();
        }
        [HttpPost]
        public IActionResult AddAvailability([FromBody] Availability availability)
        {
            if (availability == null)
                return BadRequest();

            long id = Availabilities.AddAvailability(availability);            
            return new ObjectResult(id);
        }

    }
}