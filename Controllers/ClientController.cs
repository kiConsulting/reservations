using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using reservations.Interfaces;
using reservations.Models;

namespace reservations.Controllers
{
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        public ClientController(IClientRepository clientRepository)
        {
            Clients = clientRepository;
        }

        public IClientRepository Clients { get; set; }
        
        [HttpGet("api/GetClients")]
        public IEnumerable<Client> GetAll()
        {
            return Clients.GetAll();
        }
        [HttpGet("api/GetClient/{Id}")]
        public IActionResult GetById(long Id)
        {
            var client = Clients.Find(Id);
            if (client == null)
                return NotFound();
            return new ObjectResult(client);
        }
        [HttpGet("api/GetClient/{firstName}/{lastName}")]
        public IActionResult GetByName(string? firstName, string? lastName)
        {
            var client = Clients.Find(firstName, lastName);
            if (client == null)
                return NotFound();
            return new ObjectResult(client);
        }
        [HttpGet("api/GetClientByEmail/{email}")]
        public IActionResult Find(string? email)
        {
            var client =  Clients.Find(email);
            if (client == null)
                return NotFound();
            return new ObjectResult(client);
        }   
        [HttpPost]
        public IActionResult AddClient([FromBody] Client client)
        {
            if (client == null)
                return BadRequest();

            var id = Clients.AddClient(client);
            return CreatedAtRoute("GetClient", new { id = id }, client);
        }
        [HttpDelete("{id}")]
        public IActionResult RemoveClient(long id)
        {
            var c = Clients.Find(id);
            if (c == null)
                return NotFound();
            Clients.RemoveClient(id);
            return new NoContentResult();
        }
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Client client)
        {
            if (client == null || client.Id != id)
                return BadRequest();
            var c = Clients.Find(id);
            if (c == null)
                return NotFound();
            c.FirstName = client.FirstName;
            c.LastName = client.LastName;
            c.Email = client.Email;
            c.UpdatedDate = DateTime.Now;
            Clients.Update(c);
            return new NoContentResult();
        }
    }
}