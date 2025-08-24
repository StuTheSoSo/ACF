using BackEnd.Data;
using BackEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;


        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            // DEMO METHOD TO SHOW ALL CASES IN PROGRESS AND "MY" CASES IN PROGRESS

            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var allCases = this._context.Cases.Where(x => x.Status.ToLower().Equals("active"));
            var personalCases = allCases.Where(x => x.OfficerId.ToString() == userIdString);
            
            return Ok(new StatsCollection(allCases.Count(), personalCases.Count()));
           
        }
    }
}
