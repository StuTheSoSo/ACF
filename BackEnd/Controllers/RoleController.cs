using BackEnd.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Controllers
{
    public class RoleController : ControllerBase
    {
        private readonly ILogger<RoleController> _logger;
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public RoleController(ILogger<RoleController> logger, AppDbContext context, IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _configuration = configuration;
        }


        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles()
        {
            return Ok(await _context.Roles.ToListAsync());
        }
    }
}
