using BackEnd.Data;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportsController : ControllerBase
    {
        /// <summary> The context </summary>
        private readonly AppDbContext _context;

        /// <summary> The logger </summary>
        private readonly ILogger<CaseController> _logger;

        public ReportsController(AppDbContext context, ILogger<CaseController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("officers")]
        public IActionResult GetOFficers()
        {
            // Get only officers that have cases (active or inactive) - and make it distinct by officer id
            var officersWithCases = _context.Cases
                .Where(c => c.OfficerId != null)
                .Select(c => c.OfficerId)
                .Distinct()
                .ToList();
            // Get all officers from users table that are in the above list
            var officers = _context.Users
                .Where(u => officersWithCases.Contains(u.UserId))
                .Select(u => new { u.UserId, u.FullName });
            return Ok(officers);
        }

        [HttpGet("clientsByOfficer/{officerId}")]
        public IActionResult GetClientsByOfficer(Guid officerId)
        {
            // Get distinct clients for the given officer id from cases table
            var clientIds = _context.Cases
                .Where(c => c.OfficerId == officerId)
                .Select(c => c.ClientId)
                .Distinct()
                .ToList();
            // Get client details from clients table
            var clients = _context.Clients
                .Where(cl => clientIds.Contains(cl.ClientId.Value))
                .Select(cl => new { cl.ClientId, cl.FullName });
            return Ok(clients);
        }

        [HttpGet("casesByOfficerAndClient/{officerId}/{clientId}")]
        public IActionResult GetCasesByOfficerAndClient(Guid officerId, Guid clientId)
        {
            var cases = _context.Cases
                .Where(c => c.OfficerId == officerId && c.ClientId == clientId)
                .Select(c => c);
            return Ok(cases);
        }

        [HttpGet("getUsage")]
        public IActionResult GetUsage()
        {
            var auditLogs = _context.AuditLogs.OrderBy(x => x.TimeStamp).ToList();
            foreach (var item in auditLogs)
            {
                item.ClientName = _context.Clients.FirstOrDefault(c => c.ClientId == item.ClientId)?.FullName;
                item.UserName = _context.Users.FirstOrDefault(u => u.UserId == item.UserId)?.FullName;
            }
            return Ok(auditLogs);
        }
    }
}
