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
        /// <summary> The context </summary>
        private readonly AppDbContext _context;

        /// <summary> Initializes a new instance of the <see cref="HomeController"/> class. </summary>
        /// <param name="logger">  The logger. </param>
        /// <param name="context"> The context. </param>
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary> Gets this instance. </summary>
        /// <returns> </returns>
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