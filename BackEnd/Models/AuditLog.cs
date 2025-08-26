namespace BackEnd.Models
{
    public class AuditLog
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Action { get; set; } = string.Empty;
        public Guid UserId { get; set; } = Guid.Empty;
        public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
        public string Details { get; set; } = string.Empty;
        public Guid? CaseId { get; set; } = null;

        public AuditLog()
        {
            
        }


        public AuditLog(AuditLog other)
        {
            Id = other.Id;
            Action = other.Action;
            UserId = other.UserId;
            TimeStamp = other.TimeStamp;
            Details = other.Details;
            CaseId = other.CaseId;
        }
    }
}
