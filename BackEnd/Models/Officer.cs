namespace BackEnd.Models
{
    public class Officer
    {
        public Guid OfficerId { get; set; } = Guid.Empty;

        public string? FirstName { get; set; } = string.Empty;

        public string? LastName { get; set; } = string.Empty;

        public string? Role { get; set; } = string.Empty;

        public byte[]? PasswordHash { get; set; } = Array.Empty<byte>();

        public byte[]? PasswordSalt { get; set; } = Array.Empty<byte>();

        public string Username { get; set; } = string.Empty;
    }
}
