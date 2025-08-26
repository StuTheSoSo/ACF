using BackEnd.Data;
using BackEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CaseController : ControllerBase
    {
        /// <summary> The context </summary>
        private readonly AppDbContext _context;

        /// <summary> Initializes a new instance of the <see cref="CaseController"/> class. </summary>
        /// <param name="logger">  The logger. </param>
        /// <param name="context"> The context. </param>
        public CaseController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary> Adds the case. </summary>
        /// <param name="newCase"> The new case. </param>
        /// <returns> </returns>
        [HttpPost]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> AddCase([FromBody] Case newCase)
        {
            try
            {
                newCase.CreatedDate = DateTime.UtcNow;
                newCase.UpdatedDate = DateTime.UtcNow;
                _context.Cases.Add(newCase);
                await _context.SaveChangesAsync();

                return Ok(newCase);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary> Gets the cases. </summary>
        /// <returns> </returns>
        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetCases()
        {
            // All Cases
            var allCases = _context.Cases.ToList();
            // Retrieve Client and officer names
            foreach (var tempCase in allCases)
            {
                tempCase.ClientName = _context.Clients.FirstOrDefault(c => c.ClientId == tempCase.ClientId)?.FullName;
                tempCase.OfficerName = _context.Users.FirstOrDefault(o => o.UserId == tempCase.OfficerId)?.FullName;
            }
            return Ok(_context.Cases);
        }
    }
}