using BackEnd.Data;
using BackEnd.Models;
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
        private readonly ILogger<UserController> _logger;
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        private const int SaltSize = 32; // 256-bit salt
        private const int HashSize = 64; // 512-bit hash
        private const int Iterations = 100000; // NIST-recommended iteration count

        public UserController(ILogger<UserController> logger, AppDbContext context, IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task RegisterUserAsync([FromBody]RegisterObject registerObject)
        {
            // Check if username exists
            if (await _context.Officers.AnyAsync(u => u.Username == registerObject.UserName))
                throw new Exception("Username already exists.");

            // Hash the password
            var (hash, salt) = HashPassword(registerObject.Password);

            // Create new officer
            var officer = new Officer
            {
                Username = registerObject.UserName,
                FirstName = registerObject.FirstName,
                LastName = registerObject.LastName,
                Role = registerObject.Role,
                PasswordHash = hash,
                PasswordSalt = salt
                //CreatedAt = DateTime.UtcNow
            };

            // Save to database
            _context.Officers.Add(officer);
            await _context.SaveChangesAsync();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginObject loginObject)
        {
            var token = await AuthenticateUserAsync(loginObject);
            if (token == null)
                return Unauthorized("Invalid username or password");

            return Ok(new { Token = token });
        }

        
        private async Task<string> AuthenticateUserAsync(LoginObject loginObject)
        {
            // Find officer by username
            var officer = await _context.Officers
                .FirstOrDefaultAsync(u => u.Username == loginObject.Username);

            if (officer == null)
                return null; // User not found

            // Verify password
            bool isValid = VerifyPassword(loginObject.Password, officer.PasswordHash, officer.PasswordSalt);
            if (!isValid)
                return null; // Invalid password

            // Generate JWT
            return GenerateJwtToken(officer);
        }


        private string GenerateJwtToken(Officer officer)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, officer.OfficerId.ToString()),
            new Claim(ClaimTypes.Name, officer.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
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


        private static (byte[] Hash, byte[] Salt) HashPassword(string password)
        {
            // Generate a random salt
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);

            // Hash the password using PBKDF2
            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            return (hash, salt);
        }

        private static bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt)
        {
            // Recompute the hash with the provided password and stored salt
            using var pbkdf2 = new Rfc2898DeriveBytes(password, storedSalt, Iterations, HashAlgorithmName.SHA256);
            byte[] computedHash = pbkdf2.GetBytes(HashSize);

            // Compare the computed hash with the stored hash
            return CryptographicOperations.FixedTimeEquals(computedHash, storedHash);
        }
    }
}
