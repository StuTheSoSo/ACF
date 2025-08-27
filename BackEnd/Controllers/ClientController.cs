using BackEnd.Data;
using BackEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        /// <summary> The context </summary>
        private readonly AppDbContext _context;

        private readonly ILogger<ClientController> _logger;

        /// <summary> Initializes a new instance of the <see cref="ClientController"/> class. </summary>
        /// <param name="logger">  The logger. </param>
        /// <param name="context"> The context. </param>
        public ClientController(AppDbContext context, ILogger<ClientController> logger)
        {
            _context = context;
            _logger = logger;
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
                _logger.LogInformation("Client added successfully: {ClientId} by {UserId}", client.ClientId, User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                return Ok(client);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in AddClient: {0}", ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary> Gets the clients. </summary>
        /// <returns> </returns>
        [HttpGet]
        [Authorize(Roles = "Admin, Officer, Auditor")]
        public async Task<IActionResult> GetClients()
        {
            try
            {
                _logger.LogInformation("Fetching all clients: {UserId}", User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                return Ok(_context.Clients);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in GetClients: {0}", ex.Message);
            }
            return StatusCode(500, "Internal server error");
        }
    }
}