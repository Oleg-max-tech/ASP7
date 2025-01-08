using Microsoft.AspNetCore.Mvc;
using OnlineShopProject.Models;
using OnlineShopProject.Services;
using WebApplication1.Services;


namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public IActionResult GetClients([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            var clients = _clientService.GetClients(skip, take);
            return Ok(clients);
        }

        [HttpGet("{id}")]
        public IActionResult GetClientById(int id)
        {
            var client = _clientService.GetClientById(id);
            if (client == null) return NotFound();
            return Ok(client);
        }

        [HttpPost]
        public IActionResult AddClient([FromBody] Client client)
        {
            var addedClient = _clientService.AddClient(client);
            return CreatedAtAction(nameof(GetClientById), new { id = addedClient.Id }, addedClient);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateClient(int id, [FromBody] Client client)
        {
            if (id != client.Id) return BadRequest();
            var updatedClient = _clientService.UpdateClient(client);
            return Ok(updatedClient);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteClient(int id)
        {
            _clientService.DeleteClient(id);
            return NoContent();
        }
    }
}
