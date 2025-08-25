using BackEnd.Data;
using BackEnd.Logger;
using BackEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private const int HashSize = 64;

        // 512-bit hash
        private const int Iterations = 100000;

        /// <summary> The salt size </summary>
        private const int SaltSize = 32;

        /// <summary> The configuration </summary>
        private readonly IConfiguration _configuration;

        /// <summary> The context </summary>
        private readonly AppDbContext _context;

        /// <summary> The logger </summary>
        private readonly IACFLogger _logger;

        /// <summary> Initializes a new instance of the <see cref="UserController"/> class. </summary>
        /// <param name="logger">        The logger. </param>
        /// <param name="context">       The context. </param>
        /// <param name="configuration"> The configuration. </param>
        public UserController(IACFLogger logger, AppDbContext context, IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _configuration = configuration;
        }

        /// <summary> Gets the officers. </summary>
        /// <returns> </returns>
        [HttpGet("getOfficers")]
        [Authorize]
        public async Task<IActionResult> GetOfficers()
        {
            // RoleId for officers
            var officerRoleId = _context.Roles.FirstOrDefault(x => x.RoleName!.ToLower().Equals("officer"))?.RoleId ?? Guid.Empty;
            return Ok(_context.Users.Where(x => x.RoleId == officerRoleId));
        }

        /// <summary> Logins the specified login object. </summary>
        /// <param name="loginObject"> The login object. </param>
        /// <returns> </returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginObject loginObject)
        {
            var token = await AuthenticateUserAsync(loginObject);
            if (token == null)
            {
                _logger.LogAction(new AuditLog
                {
                    Action = "Invalid username or password",
                    CaseId = Guid.Empty,
                    Details = "Invalid username or password",
                    TimeStamp = DateTime.UtcNow,
                    UserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
                });
                return Unauthorized("Invalid username or password");
            }

            return Ok(new { Token = token });
        }

        /// <summary> Registers the user asynchronous. </summary>
        /// <param name="registerObject"> The register object. </param>
        /// <exception cref="System.Exception"> Username already exists. </exception>
        [HttpPost("register")]
        public async Task RegisterUserAsync([FromBody] RegisterObject registerObject)
        {
            try
            {
                // Check if username exists
                if (await _context.Users.AnyAsync(u => u.Username == registerObject.UserName))
                    throw new Exception("Username already exists.");

                // Hash the password
                var (hash, salt) = HashPassword(registerObject.Password);

                // Create new officer
                var officer = new User
                {
                    Username = registerObject.UserName,
                    FirstName = registerObject.FirstName,
                    LastName = registerObject.LastName,
                    RoleId = registerObject.Role,
                    PasswordHash = hash,
                    PasswordSalt = salt
                };

                // Save to database
                _context.Users.Add(officer);
                await _context.SaveChangesAsync();
                _logger.LogAction(new AuditLog
                {
                    Action = "New User Registered",
                    CaseId = Guid.Empty,
                    Details = "New User Registered",
                    TimeStamp = DateTime.UtcNow,
                    UserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
                });
            }
            catch (Exception ex)
            {
                var stop = ex;
            }
        }

        /// <summary> Hashes the password. </summary>
        /// <param name="password"> The password. </param>
        /// <returns> </returns>
        private static (byte[] Hash, byte[] Salt) HashPassword(string password)
        {
            // Generate a random salt
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);

            // Hash the password using PBKDF2
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            return (hash, salt);
        }

        /// <summary> Verifies the password. </summary>
        /// <param name="password">   The password. </param>
        /// <param name="storedHash"> The stored hash. </param>
        /// <param name="storedSalt"> The stored salt. </param>
        /// <returns> </returns>
        private static bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt)
        {
            // Recompute the hash with the provided password and stored salt
            using var pbkdf2 = new Rfc2898DeriveBytes(password, storedSalt, Iterations, HashAlgorithmName.SHA256);
            byte[] computedHash = pbkdf2.GetBytes(HashSize);

            // Compare the computed hash with the stored hash
            return CryptographicOperations.FixedTimeEquals(computedHash, storedHash);
        }

        /// <summary> Authenticates the user asynchronous. </summary>
        /// <param name="loginObject"> The login object. </param>
        /// <returns> </returns>
        private async Task<string> AuthenticateUserAsync(LoginObject loginObject)
        {
            // Find officer by username
            var officer = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == loginObject.Username);

            if (officer == null)
                return null; // User not found

            // Verify password
            bool isValid = VerifyPassword(loginObject.Password, officer.PasswordHash, officer.PasswordSalt);
            if (!isValid)
                return null; // Invalid password

            // Generate JWT
            return await GenerateJwtToken(officer);
        }

        /// <summary> Generates the JWT token. </summary>
        /// <param name="officer"> The officer. </param>
        /// <returns> </returns>
        private async Task<string> GenerateJwtToken(User officer)
        {
            var role = await _context.Roles.FindAsync(officer.RoleId);

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, officer.UserId.ToString()),
            new Claim(ClaimTypes.Name, officer.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, role?.RoleName ?? "User")
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Jwt:ExpiryMinutes"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiry,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}