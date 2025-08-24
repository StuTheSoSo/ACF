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
        private readonly ILogger<CaseController> _logger;
        private readonly AppDbContext _context;

        public CaseController(ILogger<CaseController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCases()
        {
            // All Cases
            var allCases = _context.Cases.ToList();
            // Retrieve Client and officer names
            foreach(var tempCase in allCases)
            {
                tempCase.ClientName = _context.Clients.FirstOrDefault(c => c.ClientId == tempCase.ClientId)?.FullName;
                tempCase.OfficerName = _context.Officers.FirstOrDefault(o => o.OfficerId == tempCase.OfficerId)?.FullName;
            }
            return Ok(_context.Cases);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddCase([FromBody]Case newCase)
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
                _logger.LogError(ex, "Error adding client");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
