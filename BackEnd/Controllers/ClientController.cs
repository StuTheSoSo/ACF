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
        /// <summary> The context </summary>
        private readonly AppDbContext _context;

        /// <summary> The logger </summary>
        private readonly ILogger<ClientController> _logger;

        /// <summary> Initializes a new instance of the <see cref="ClientController"/> class. </summary>
        /// <param name="logger">  The logger. </param>
        /// <param name="context"> The context. </param>
        public ClientController(ILogger<ClientController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary> Adds the client. </summary>
        /// <param name="client"> The client. </param>
        /// <returns> </returns>
        [HttpPost]
        [Authorize(Roles = "Admin, Officer")]
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

        /// <summary> Gets the clients. </summary>
        /// <returns> </returns>
        [HttpGet]
        [Authorize(Roles = "Admin, Officer, Auditor")]
        public async Task<IActionResult> GetClients()
        {
            return Ok(_context.Clients);
        }
    }
}