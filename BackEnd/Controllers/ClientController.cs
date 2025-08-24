using BackEnd.Data;
using BackEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> _logger;
        private readonly AppDbContext _context;

        public ClientController(ILogger<ClientController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddClient([FromBody] Client client)
        {
            try
            {
                _context.Clients.Add(client);
                await _context.SaveChangesAsync();
                return Ok(client);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding client");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetClients()
        {
            return Ok(_context.Clients);
        }

    }
}
