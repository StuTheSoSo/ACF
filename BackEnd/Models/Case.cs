using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models
{
    public class Case
    {
        /// <summary> Gets or sets the case identifier. </summary>
        /// <value> The case identifier. </value>
        public Guid CaseId { get; set; } = Guid.Empty;

        /// <summary> Gets or sets the client identifier. </summary>
        /// <value> The client identifier. </value>
        public Guid ClientId { get; set; } = Guid.Empty;

        /// <summary> Gets or sets the name of the client. </summary>
        /// <value> The name of the client. </value>
        [NotMapped]
        public string? ClientName { get; set; } = string.Empty;

        /// <summary> Gets or sets the created date. </summary>
        /// <value> The created date. </value>
        public DateTime? CreatedDate { get; set; } = DateTime.MinValue;

        /// <summary> Gets or sets the officer identifier. </summary>
        /// <value> The officer identifier. </value>
        public Guid OfficerId { get; set; } = Guid.Empty;

        /// <summary> Gets or sets the name of the officer. </summary>
        /// <value> The name of the officer. </value>
        [NotMapped]
        public string? OfficerName { get; set; } = string.Empty;

        /// <summary> Gets or sets the recommendations. </summary>
        /// <value> The recommendations. </value>
        public string Recommendations { get; set; } = string.Empty;

        /// <summary> Gets or sets the region. </summary>
        /// <value> The region. </value>
        public string Region { get; set; } = string.Empty;

        /// <summary> Gets or sets the status. </summary>
        /// <value> The status. </value>
        public string Status { get; set; } = string.Empty;

        /// <summary> Gets or sets the updated date. </summary>
        /// <value> The updated date. </value>
        public DateTime? UpdatedDate { get; set; } = DateTime.MinValue;
    }
}