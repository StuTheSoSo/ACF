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
    public class CaseController : ControllerBase
    {
        /// <summary> The context </summary>
        private readonly AppDbContext _context;

        /// <summary> The logger </summary>
        private IACFLogger _logger;

        /// <summary> Initializes a new instance of the <see cref="CaseController"/> class. </summary>
        /// <param name="logger">  The logger. </param>
        /// <param name="context"> The context. </param>
        public CaseController(AppDbContext context, IACFLogger logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary> Adds the case. </summary>
        /// <param name="newCase"> The new case. </param>
        /// <returns> </returns>
        [HttpPost]
        [Authorize(Roles = "Admin, Officer")]
        public async Task<IActionResult> AddCase([FromBody] Case newCase)
        {
            try
            {
                newCase.CreatedDate = DateTime.UtcNow;
                newCase.UpdatedDate = DateTime.UtcNow;
                _context.Cases.Add(newCase);
                await _context.SaveChangesAsync();
                _logger.LogAction(new AuditLog
                {
                    Action = "Add Case",
                    CaseId = newCase.CaseId,
                    Details = "Case Added",
                    TimeStamp = DateTime.UtcNow,
                    UserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
                });

                return Ok(newCase);
            }
            catch (Exception ex)
            {
                _logger.LogAction(new AuditLog
                {
                    Action = "Error Adding Case",
                    CaseId = newCase.CaseId,
                    Details = ex.Message,
                    TimeStamp = DateTime.UtcNow,
                    UserId = Guid.Parse(User.Claims.First(c => c.Type == "UserId").Value)
                });
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary> Gets the cases. </summary>
        /// <returns> </returns>
        [HttpGet]
        [Authorize(Roles = "Admin, Officer")]
        public async Task<IActionResult> GetCases()
        {
            // All Cases
            var allCases = _context.Cases.ToList();
            // Retrieve Client and officer names
            foreach (var tempCase in allCases)
            {
                tempCase.ClientName = _context.Clients.FirstOrDefault(c => c.ClientId == tempCase.ClientId)?.FullName;
                tempCase.OfficerName = _context.Officers.FirstOrDefault(o => o.OfficerId == tempCase.OfficerId)?.FullName;
            }
            return Ok(_context.Cases);
        }
    }
}