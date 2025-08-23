namespace BackEnd.Models
{
    public class RegisterObject
    {
        public string UserName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public Guid Role { get; set; } = Guid.Empty;
        public string Password { get; set; } = string.Empty;

    }
}
