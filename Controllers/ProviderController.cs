using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using reservations.Interfaces;
using reservations.Models;

namespace reservations.Controllers
{
    [Route("[controller]")]
    public class ProviderController : ControllerBase
    {
        public ProviderController(IProviderRepository ProviderRepository)
        {
            Providers = ProviderRepository;
        }

        public IProviderRepository Providers { get; set; }
        
 
        [HttpGet("api/GetProviders")]
        public IEnumerable<Provider> GetAll()
        {
            return Providers.GetAll();
        }
        [HttpGet("api/GetProvidersBySpeciality/{specialty}")]
        public IEnumerable<Provider> GetBySpecialty(string? specialty)
        {
            return Providers.GetBySpecialty(specialty);
        }
        [HttpGet("api/GetProvider/{Id}")]
        public IActionResult Find(long Id)
        {
            var p = Providers.Find(Id);
            if (p == null)
                return NotFound();
            return new ObjectResult(p);
        }
        [HttpGet("api/GetProviderByName/{FirstName}/{LastName}")]
        public IActionResult Find(string? FirstName, string? LastName)
        {
            if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName))
                return BadRequest();
            var p = Providers.Find(FirstName, LastName);
            if (p == null)
                return NotFound();
            return new ObjectResult(p);
        }
        [HttpDelete("{id}")]
        public IActionResult RemoveProvider(long id)
        {
            var p = Providers.Find(id);
            if (p == null)
                return NotFound();

            Providers.RemoveProvider(id);
            return new NoContentResult();
        }
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Provider provider)
        {
            if (provider == null || provider.Id != id)
                return BadRequest();
            
            var p = Providers.Find(id);
            if (p == null)
                return NotFound();
            
            p.FirstName = provider.FirstName;
            p.LastName = provider.LastName;
            p.Specialty = provider.Specialty;
            p.UpdatedDate = DateTime.Now;

            Providers.Update(p);
            return new NoContentResult();
        }
        [HttpPost("api/AddProvider")]
        public IActionResult AddProvider([FromBody] Provider provider)
        {
            if (provider == null)
                return BadRequest();
            
            long id = Providers.AddProvider(provider);
            return new ObjectResult(id);
        }
    }
}