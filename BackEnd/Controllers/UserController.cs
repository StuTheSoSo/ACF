using BackEnd.Data;
using BackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly AppDbContext _context;

        private const int SaltSize = 32; // 256-bit salt
        private const int HashSize = 64; // 512-bit hash
        private const int Iterations = 100000; // NIST-recommended iteration count

        public UserController(ILogger<UserController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost("register")]
        public async Task RegisterUserAsync(string username, string password)
        {
            // Check if username exists
            if (await _context.Officers.AnyAsync(u => u.Username == username))
                throw new Exception("Username already exists.");

            // Hash the password
            var (hash, salt) = HashPassword(password);

            // Create new user
            var user = new Officer
            {
                Username = username,
                PasswordHash = hash,
                PasswordSalt = salt
                //CreatedAt = DateTime.UtcNow
            };

            // Save to database
            _context.Officers.Add(user);
            await _context.SaveChangesAsync();
        }

        [HttpGet("authenticate")]
        public async Task<Officer> AuthenticateUserAsync(string username, string password)
        {
            // Find user by username
            var user = await _context.Officers
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
                return null; // User not found

            // Verify password
            bool isValid = VerifyPassword(password, user.PasswordHash, user.PasswordSalt);

            return isValid ? user : null;
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


        [HttpGet(Name = "GetUsers")]
        public async Task<ActionResult> GetUsers()
        {
            var test = _context.Officers;
            return Ok(test);
        }

    }
}
