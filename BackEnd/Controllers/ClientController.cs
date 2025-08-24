using BackEnd.Data;
using BackEnd.Logger;
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

        /// <summary> The logger </summary>
        private readonly IACFLogger _logger;

        /// <summary> Initializes a new instance of the <see cref="ClientController"/> class. </summary>
        /// <param name="logger">  The logger. </param>
        /// <param name="context"> The context. </param>
        public ClientController(IACFLogger logger, AppDbContext context)
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
                _logger.LogAction(new AuditLog
                {
                    Action = "Add Client",
                    CaseId = Guid.Empty,
                    Details = "Client Added",
                    TimeStamp = DateTime.UtcNow,
                    UserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
                });
                return Ok(client);
            }
            catch (Exception ex)
            {
                _logger.LogAction(new AuditLog
                {
                    Action = "Error Adding Client",
                    CaseId = Guid.Empty,
                    Details = ex.Message,
                    TimeStamp = DateTime.UtcNow,
                    UserId = Guid.Parse(User.Claims.First(c => c.Type == "UserId").Value)
                });
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