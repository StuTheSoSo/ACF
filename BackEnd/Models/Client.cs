namespace BackEnd.Models
{
    public class Client
    {
        public Guid? ClientId { get; set; } = Guid.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string SSN { get; set; } = string.Empty;
        public string Eligibility { get; set; } = string.Empty;
    }
}
