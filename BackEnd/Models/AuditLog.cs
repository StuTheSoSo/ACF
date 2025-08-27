using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models
{
    public class AuditLog
    {
        public int Id { get; set; }
        public string? Message { get; set; } = string.Empty;

        public string? MessageTemplate { get; set; } = string.Empty;
        public string? Level { get; set; } = string.Empty;
        public DateTime? TimeStamp { get; set; } = DateTime.UtcNow;
        public string? Exception { get; set; } = string.Empty;
        public string? Properties { get; set; } = string.Empty;
        public Guid? CaseId { get; set; } = Guid.Empty;
        public Guid? ClientId { get; set; } = Guid.Empty;
        [NotMapped]
        public string? ClientName { get; set; } = string.Empty;
        public Guid? UserId { get; set; } = Guid.Empty;
        [NotMapped]
        public string? UserName { get; set; } = string.Empty;


    }
}
