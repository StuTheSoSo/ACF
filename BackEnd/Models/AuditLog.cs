using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models
{
    public class AuditLog
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }
        /// <summary>Gets or sets the message.</summary>
        /// <value>The message.</value>
        public string? Message { get; set; } = string.Empty;

        /// <summary>Gets or sets the message template.</summary>
        /// <value>The message template.</value>
        public string? MessageTemplate { get; set; } = string.Empty;
        /// <summary>Gets or sets the level.</summary>
        /// <value>The level.</value>
        public string? Level { get; set; } = string.Empty;
        /// <summary>Gets or sets the time stamp.</summary>
        /// <value>The time stamp.</value>
        public DateTime? TimeStamp { get; set; } = DateTime.UtcNow;
        /// <summary>Gets or sets the exception.</summary>
        /// <value>The exception.</value>
        public string? Exception { get; set; } = string.Empty;
        /// <summary>Gets or sets the properties.</summary>
        /// <value>The properties.</value>
        public string? Properties { get; set; } = string.Empty;
        /// <summary>Gets or sets the case identifier.</summary>
        /// <value>The case identifier.</value>
        public Guid? CaseId { get; set; } = Guid.Empty;
        /// <summary>Gets or sets the client identifier.</summary>
        /// <value>The client identifier.</value>
        public Guid? ClientId { get; set; } = Guid.Empty;
        [NotMapped]
        public string? ClientName { get; set; } = string.Empty;
        /// <summary>Gets or sets the user identifier.</summary>
        /// <value>The user identifier.</value>
        public Guid? UserId { get; set; } = Guid.Empty;
        /// <summary>Gets or sets the name of the user.</summary>
        /// <value>The name of the user.</value>
        [NotMapped]
        public string? UserName { get; set; } = string.Empty;


    }
}
