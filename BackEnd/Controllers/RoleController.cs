using BackEnd.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController : ControllerBase
    {
        /// <summary> The context </summary>
        private readonly AppDbContext _context;

        /// <summary> The logger </summary>
        private readonly ILogger<RoleController> _logger;

        /// <summary> Initializes a new instance of the <see cref="RoleController"/> class. </summary>
        /// <param name="logger">  The logger. </param>
        /// <param name="context"> The context. </param>
        public RoleController(ILogger<RoleController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <summary> Gets the roles. </summary>
        /// <returns> </returns>
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            return Ok(await _context.Roles.ToListAsync());
        }
    }
}