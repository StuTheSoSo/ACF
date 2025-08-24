namespace BackEnd.Models
{
    public class Case
    {
        public Guid CaseId { get; set; } = Guid.Empty;

        public Guid ClientId { get; set; } = Guid.Empty;
        public Guid OfficerId { get; set; } = Guid.Empty;
        public string Status { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public string Recommendations { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.MinValue;
        public DateTime UpdatedDate { get; set; } = DateTime.MinValue;
    }
}
